Partial Public Class DownloadAssembly
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdDownload_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim uri As String = Application.Current.Host.Source.AbsoluteUri
        Dim index As Integer = uri.IndexOf("/ClientBin")
        ' In this example, the URI includes the /ClientBin portion, because we've
        ' decided to place the DLL in the ClientBin folder.
        uri = uri.Substring(0, index) & "/ClientBin/ResourceClassLibrary2.dll"

        ' Begin the download.
        Dim webClient As New WebClient()
        AddHandler webClient.OpenReadCompleted, AddressOf webClient_OpenReadCompleted
        webClient.OpenReadAsync(New Uri(uri))
    End Sub

    Private Sub webClient_OpenReadCompleted(ByVal sender As Object, ByVal e As OpenReadCompletedEventArgs)
        Dim assemblypart As New AssemblyPart()
        assemblypart.Load(e.Result)
        txt.Text = "Assembly downloaded. You can now use it."
    End Sub

    Private Sub cmdUseAssembly_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' You can't catch errors here.
        ' If the assembly was not loaded, look for a FileNotFoundException in Application.UnhandledException event.

        Dim util As New ResourceClassLibrary2.ClassLibraryUtil()
        txt.Text = util.DoSomething()
    End Sub

End Class
