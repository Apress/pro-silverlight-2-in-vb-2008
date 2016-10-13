Imports System.Net
Imports System.IO
Imports System.Windows.Browser

Public Partial Class CallRestPage
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub


    Private searchYear As String

    Private Sub cmdGetData_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim address As New Uri("http://localhost:" & HtmlPage.Document.DocumentUri.Port & "/Networking.Web/PopulationService.ashx")

        Dim request As WebRequest = WebRequest.Create(address)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        ' Store the year you want to use.
        searchYear = txtYear.Text

        ' Prepare the request asynchronously.
        request.BeginGetRequestStream(AddressOf CreateRequest, request)
    End Sub

    Private Sub CreateRequest(ByVal asyncResult As IAsyncResult)
        Dim request As WebRequest = CType(asyncResult.AsyncState, WebRequest)

        ' Write the year information in the name-value format "year=1985".
        Dim requestStream As Stream = request.EndGetRequestStream(asyncResult)
        Dim writer As New StreamWriter(requestStream)
        writer.Write("year=" & searchYear)

        ' Clean up (required).
        writer.Close()
        requestStream.Close()

        ' Read the response asynchronously.
        request.BeginGetResponse(AddressOf ReadResponse, request)
    End Sub

    Private Sub ReadResponse(ByVal asyncResult As IAsyncResult)
        Dim result As String
        Dim request As WebRequest = CType(asyncResult.AsyncState, WebRequest)

        ' Get the respone stream.
        Dim response As WebResponse = request.EndGetResponse(asyncResult)
        Dim responseStream As Stream = response.GetResponseStream()

        Try
            ' Read the returned text.
            Dim reader As New StreamReader(responseStream)
            Dim population As String = reader.ReadToEnd()
            result = population & " people."
        Catch err As Exception
            result = "Error contacting service."
        Finally
            response.Close()
        End Try

        ' Update the display.
        Dispatcher.BeginInvoke(New Action(Of String)(AddressOf UpdateText), result)
                    
    End Sub

    Private Sub UpdateText(ByVal text As String)
        lblResult.Text = text
    End Sub


End Class
