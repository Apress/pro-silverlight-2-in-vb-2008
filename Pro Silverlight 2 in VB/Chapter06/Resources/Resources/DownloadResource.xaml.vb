Imports System.IO

Partial Public Class DownloadResource
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdDownload_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim uri As String = App.Current.Host.Source.AbsoluteUri
        Dim index As Integer = uri.IndexOf("/ClientBin")
        uri = uri.Substring(0, index) & "/ProductList.xml"

        Dim webClient As New WebClient()
        AddHandler webClient.OpenReadCompleted, AddressOf webClient_OpenReadCompleted
        AddHandler webClient.DownloadProgressChanged, AddressOf webClient_DownloadProgressChanged
        webClient.OpenReadAsync(New Uri(uri))
    End Sub

    Private Sub webClient_OpenReadCompleted(ByVal sender As Object, ByVal e As OpenReadCompletedEventArgs)
        Dim stream As Stream = CType(e.Result, Stream)
        Dim reader As XmlReader = XmlReader.Create(stream, New XmlReaderSettings())

        txt.Text = "Here's a dump of the retrieved data:" & Constants.vbLf + Constants.vbLf
        Do While reader.Read()
            txt.Text += reader.Value
        Loop
        reader.Close()
    End Sub

    Private Sub webClient_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        lblProgress.Text = e.ProgressPercentage.ToString() & " % downloaded."
        progressBar.Value = e.ProgressPercentage
    End Sub

End Class
