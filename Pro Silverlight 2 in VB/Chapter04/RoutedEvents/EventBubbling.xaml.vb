Partial Public Class EventBubbling
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Protected eventCounter As Integer = 0

    Private Sub SomethingClicked(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        eventCounter += 1
        '" Source: " + e.Source + "\r\n"; 
        Dim message As String = "#" & eventCounter.ToString() & ":" & Environment.NewLine & _
          " Sender: " & sender.ToString() & Environment.NewLine & " Handled: " & e.Handled & Environment.NewLine
        lstMessages.Items.Add(message)
    End Sub

    Private Sub cmdClear_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        lstMessages.Items.Clear()
        eventCounter = 0
    End Sub
End Class
