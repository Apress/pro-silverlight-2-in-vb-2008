Partial Public Class SimplePopup
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub txt_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        popUp.IsOpen = True
    End Sub

    Private Sub popUp_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        popUp.IsOpen = False
    End Sub
End Class
