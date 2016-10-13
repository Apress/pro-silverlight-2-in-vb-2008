Imports System.Windows.Browser

Partial Public Class BrowserDetails
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub UserControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim b As BrowserInformation = HtmlPage.BrowserInformation
        lblInfo.Text = "Name: " & b.Name
        lblInfo.Text += Constants.vbLf & "Browser Version: " & b.BrowserVersion.ToString()
        lblInfo.Text += Constants.vbLf & "Platform: " & b.Platform
        lblInfo.Text += Constants.vbLf & "Cookies Enabled: " & b.CookiesEnabled
        lblInfo.Text += Constants.vbLf & "User Agent: " & b.UserAgent
    End Sub
End Class
