Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports DataBinding.DataService
Imports System.Windows.Browser

Public Partial Class ValueConverters
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private products As ObservableCollection(Of Product) = New ObservableCollection(Of Product)()

    Private Sub cmdGetProducts_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim address As New EndpointAddress("http://localhost:" & HtmlPage.Document.DocumentUri.Port & "/DataBinding.Web/StoreDb.svc")
        Dim client As New StoreDbClient(New BasicHttpBinding(), address)

        AddHandler client.GetProductsCompleted, AddressOf client_GetProductsCompleted
        client.GetProductsAsync()
    End Sub

    Private Sub client_GetProductsCompleted(ByVal sender As Object, ByVal e As GetProductsCompletedEventArgs)
        Try
            products.Clear()
            For Each product As Product In e.Result
            products.Add(product)
            Next

            lstProducts.ItemsSource = products
        Catch err As Exception
            lblError.Text = "Failed to contact service."
        End Try
    End Sub

    Private Sub lstProducts_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
        borderProductDetails.DataContext = lstProducts.SelectedItem
    End Sub
End Class
