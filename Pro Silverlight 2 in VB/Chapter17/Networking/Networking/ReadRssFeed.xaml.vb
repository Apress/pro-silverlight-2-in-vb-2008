Imports System.Xml
Imports System.ServiceModel.Syndication
Imports System.Net
Imports System.Windows.Browser

Public Partial Class ReadRssFeed
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub cmdGetData_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim client As New WebClient()
        Dim address As New Uri("http://feeds.feedburner.com/ZDNetBlogs")
        AddHandler client.OpenReadCompleted, AddressOf client_OpenReadCompleted
        client.OpenReadAsync(address)
    End Sub

    Private Sub client_OpenReadCompleted(ByVal sender As Object, ByVal e As OpenReadCompletedEventArgs)
        Try
            Dim reader As XmlReader = XmlReader.Create(e.Result)
            Dim feed As SyndicationFeed = SyndicationFeed.Load(reader)
            gridFeed.ItemsSource = feed.Items
            reader.Close()
        Catch err As Exception

        End Try
    End Sub

    Private Sub gridFeed_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim element As HtmlElement = HtmlPage.Document.GetElementById("rssFrame")
        Dim selectedItem As SyndicationItem = CType(gridFeed.SelectedItem, SyndicationItem)
        element.SetAttribute("src", selectedItem.Links(0).Uri.ToString())
    End Sub



End Class
