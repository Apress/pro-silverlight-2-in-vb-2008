Imports System.Net
Imports System.Windows.Browser
Imports System.Text.RegularExpressions

Public Partial Class ReadHtmlPage
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdGetData_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        lblResult.Text = ""

        Dim client As New WebClient()
        Dim address As New Uri("http://localhost:" & HtmlPage.Document.DocumentUri.Port & "/Networking.Web/PopulationTable.html")

        AddHandler client.DownloadStringCompleted, AddressOf client_DownloadStringCompleted
        client.DownloadStringAsync(address)
    End Sub

    Private Sub client_DownloadStringCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        Dim pageHtml As String = ""

        Try
            pageHtml = e.Result
        Catch
            lblResult.Text = "Error contacting service."
            Return
        End Try

        ' Match in form <th>500 BCE</th><td>100,000</td>
        Dim pattern As String = "<th>" & txtYear.Text & "</th>" & "\s*" & "<td>" & "(?<population>.*)" & "</td>"

        Dim regex As New Regex(pattern)
        Dim match As Match = regex.Match(pageHtml)
        Dim people As String = match.Groups("population").Value
        If people = "" Then
            lblResult.Text = "Year not found."
        Else
            lblResult.Text = match.Groups("population").Value & " people."
        End If
    End Sub
End Class
