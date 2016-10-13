Imports System.Net
Imports System.Xml.Linq
Imports System.Windows.Media.Imaging
Imports System.IO
Imports System.Xml
Imports System.Windows.Browser

Public Partial Class CallFlicker
    Inherits UserControl
    Public Sub New()
        InitializeComponent()

    End Sub

    ' NOTE: FOR THESE EXAMPLES TO WORK, YOU MUST SIGN UP FOR YOUR OWN API KEY.
    ' Enter the API key in the places where you find the ... in the code below.

    Private Sub cmdGetDataRest_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim client As New WebClient()
        Dim address As New Uri("http://api.flickr.com/services/rest/?" & "method=flickr.photos.search" & "&tags=" & HttpUtility.UrlEncode(txtSearchKeyword.Text) & "&api_key=..." & "&perpage=10")

        AddHandler client.DownloadStringCompleted, AddressOf client_DownloadStringCompleted
        client.DownloadStringAsync(address)
    End Sub

    Private Sub client_DownloadStringCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        Dim document As XDocument = XDocument.Parse(e.Result)

        ' Approach 1 (dynamic control creation).
        If Not images.ItemsSource Is Nothing Then
            images.ItemsSource = Nothing
        End If
        images.Items.Clear()
        images.ItemTemplate = Nothing
        For Each element As XElement In document.Descendants("photo")
                Dim imageUrl As String = String.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg", CStr(element.Attribute("farm")), CStr(element.Attribute("server")), CStr(element.Attribute("id")), CStr(element.Attribute("secret")))
                Dim img As New Image()
                img.Stretch = Stretch.Uniform
                img.Width = 200
                img.Height = 200
                img.Margin = New Thickness(10)
                img.Source = New BitmapImage(New Uri(imageUrl))
                images.Items.Add(img)
        Next
        Return

        ' Approach 2 (LINQ to XML).
        Dim photos = _
            From results In document.Descendants("photo") _
            Select New FlickrImage With {.imageUrl = _
              String.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg", _
              results.Attribute("farm").Value.ToString(), _
              results.Attribute("server").Value.ToString(), _
              results.Attribute("id").Value.ToString(), _
              results.Attribute("secret").Value.ToString())}

        images.ItemTemplate = CType(Me.Resources("imageTemplate"), DataTemplate)
        images.ItemsSource = photos

    End Sub

    Private searchKeyword As String

    Private Sub cmdGetDataXmlHttp_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim address As New Uri("http://api.flickr.com/services/xmlrpc/")
        Dim request As WebRequest = WebRequest.Create(address)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        ' Prepare the request asynchronously.
        searchKeyword = HttpUtility.HtmlEncode(txtSearchKeyword.Text)
        request.BeginGetRequestStream(AddressOf CreateRequest, request)
    End Sub

    Private Sub CreateRequest(ByVal asyncResult As IAsyncResult)
        Dim request As WebRequest = CType(asyncResult.AsyncState, WebRequest)

        Dim requestStream As Stream = request.EndGetRequestStream(asyncResult)

        Dim writer As New StreamWriter(requestStream)
        writer.Write(<methodCall>
                         <methodName>flickr.photos.search</methodName>
                         <params>
                             <param>
                                 <value>
                                     <struct>
                                         <member>
                                             <name>tags</name>
                                             <value><string><%= searchKeyword %></string></value>
                                         </member>
                                         <member>
                                             <name>api_key</name>
                                             <value><string>...</string></value>
                                         </member>
                                         <member>
                                             <name>perpage</name>
                                             <value><string>10</string></value>
                                         </member>

                                     </struct>
                                 </value>
                             </param>
                         </params>
                     </methodCall>)

        writer.Close()
        requestStream.Close()

        ' Read the response asynchronously.
        request.BeginGetResponse(AddressOf ReadResponse, request)
    End Sub

    Private Sub ReadResponse(ByVal asyncResult As IAsyncResult)
        Dim request As WebRequest = CType(asyncResult.AsyncState, WebRequest)

        ' Get the respone stream.
        Dim response As WebResponse = request.EndGetResponse(asyncResult)
        Dim responseStream As Stream = response.GetResponseStream()


        ' Read the returned text.
        Dim reader As New StreamReader(responseStream)
        Dim responseText As String = HttpUtility.HtmlDecode(reader.ReadToEnd())
        response.Close()
        Dim document As XDocument = XDocument.Parse(responseText)

        Dim photos = From results In document.Descendants("photo") _
                     Select New FlickrImage With {.imageUrl = _
                       String.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg", _
                       results.Attribute("farm").Value.ToString(), _
                       results.Attribute("server").Value.ToString(), _
                       results.Attribute("id").Value.ToString(), _
                       results.Attribute("secret").Value.ToString())}

        Dispatcher.BeginInvoke(New Action(Of Object)(AddressOf ReadResponseUpdate), photos)

    End Sub

    Private Sub ReadResponseUpdate(ByVal photos)
        If Not images.ItemsSource Is Nothing Then
            images.ItemsSource = Nothing
        End If
        images.Items.Clear()
        images.ItemTemplate = CType(Me.Resources("imageTemplate"), DataTemplate)
        images.ItemsSource = photos
    End Sub

End Class

Public Class FlickrImage
    Private _imageUrl As String
    Public Property ImageUrl() As String
        Get
            Return _imageUrl
        End Get
        Set(ByVal value As String)
            _imageUrl = value
        End Set
    End Property

End Class


