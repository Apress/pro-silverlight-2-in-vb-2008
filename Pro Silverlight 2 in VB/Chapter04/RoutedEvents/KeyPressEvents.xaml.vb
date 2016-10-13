Partial Public Class KeyPressEvents
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub txt_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs)
        Dim message As String = "KeyUp " & " Key: " & e.Key
        lstMessages.Items.Add(message)
    End Sub
    Private Sub txt_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        Dim message As String = "KeyDown " & " Key: " & e.Key
        lstMessages.Items.Add(message)
    End Sub

    Private Sub txt_TextChanged(ByVal sender As Object, ByVal e As TextChangedEventArgs)
        Dim message As String = "TextChanged"
        lstMessages.Items.Add(message)
    End Sub

    Private Sub cmdClear_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        lstMessages.Items.Clear()
    End Sub

End Class
