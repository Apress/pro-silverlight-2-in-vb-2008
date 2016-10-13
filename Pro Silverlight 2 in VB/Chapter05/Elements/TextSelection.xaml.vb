Partial Public Class TextSelection
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub txt_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
        If txtSelection IsNot Nothing Then
            txtSelection.Text = String.Format("Selection from {0} to {1} is ""{2}""", _
              txt.SelectionStart, txt.SelectionLength, txt.SelectedText)
        End If
    End Sub
End Class
