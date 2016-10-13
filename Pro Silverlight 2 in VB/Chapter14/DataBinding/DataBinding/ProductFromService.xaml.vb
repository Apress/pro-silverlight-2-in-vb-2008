Imports DataBinding.DataService
Imports System.Windows.Browser
Imports System.ServiceModel

Public Partial Class ProductFromService
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdGetProduct_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim address As New EndpointAddress("http://localhost:" & HtmlPage.Document.DocumentUri.Port & "/DataBinding.Web/StoreDb.svc")
        Dim client As New StoreDbClient(New BasicHttpBinding(), address)

        AddHandler client.GetProductCompleted, AddressOf client_GetProductCompleted
        client.GetProductAsync(356)
    End Sub

    Private Sub client_GetProductCompleted(ByVal sender As Object, ByVal e As GetProductCompletedEventArgs)
        Try
            gridProductDetails.DataContext = e.Result
        Catch err As Exception
            lblError.Text = "Failed to contact service."
        End Try
    End Sub
End Class
