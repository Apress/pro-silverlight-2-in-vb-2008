Imports System.Runtime.Serialization.Json
Imports System.IO
Imports System.Net
Imports System.Windows.Browser

Public Partial Class CallJsonService
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

     Private Sub cmdGetData_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim client As New WebClient()
        Dim address As New Uri("http://search.yahooapis.com/ImageSearchService/V1/imageSearch?appid=YahooDemo&query=" & HttpUtility.UrlEncode(txtSearchKeyword.Text) & "&output=json")

        AddHandler client.OpenReadCompleted, AddressOf client_OpenReadCompleted
        client.OpenReadAsync(address)
     End Sub

     Private Sub client_OpenReadCompleted(ByVal sender As Object, ByVal e As OpenReadCompletedEventArgs)
         Dim serializer As New DataContractJsonSerializer(GetType(SearchResults))
         Dim results As SearchResults = CType(serializer.ReadObject(e.Result), SearchResults)

         lblResultsTotal.Text = results.ResultSet.totalResultsAvailable & " total results."
         lblResultsReturned.Text = results.ResultSet.totalResultsReturned & " results returned."
         gridResults.ItemsSource = results.ResultSet.Result
     End Sub
End Class

Public Class SearchResults
    Public ResultSet As SearchResultSet
End Class

Public Class SearchResultSet
    Private _totalResultsAvailable As Integer
    Public Property totalResultsAvailable() As Integer
        Get
            Return _totalResultsAvailable
        End Get
        Set(ByVal value As Integer)
            _totalResultsAvailable = value
        End Set
    End Property

    Private _totalResultsReturned As Integer
    Public Property totalResultsReturned() As Integer
        Get
            Return _totalResultsReturned
        End Get
        Set(ByVal value As Integer)
            _totalResultsReturned = value
        End Set
    End Property

    Private _result As SearchResult()
    Public Property Result() As SearchResult()
        Get
            Return _result
        End Get
        Set(ByVal value As SearchResult())
            _result = value
        End Set
    End Property
End Class

Public Class SearchResult
    Private _title As String
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Private _summary As String
    Public Property Summary() As String
        Get
            Return _summary
        End Get
        Set(ByVal value As String)
            _summary = value
        End Set
    End Property

    Private _url As String
    Public Property Url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property
End Class
