Imports System.IO.IsolatedStorage

Public Partial Class ApplicationSettings
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        IsolatedStorageSettings.ApplicationSettings("LastRunDate") = DateTime.Now
        lblData.Text = "Saved."
    End Sub

    Private Sub cmdRead_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        If IsolatedStorageSettings.ApplicationSettings.Contains("LastRunDate") Then
            Dim [date] As DateTime = CDate(IsolatedStorageSettings.ApplicationSettings("LastRunDate"))
            lblData.Text = [date].ToShortTimeString()
        End If
    End Sub
End Class
