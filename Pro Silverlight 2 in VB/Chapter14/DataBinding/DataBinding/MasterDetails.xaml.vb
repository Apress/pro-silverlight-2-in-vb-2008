Imports System.ServiceModel
Imports System.Windows.Browser
Imports DataBinding.DataService

Public Partial Class MasterDetails
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim address As New EndpointAddress("http://localhost:" & HtmlPage.Document.DocumentUri.Port & "/DataBinding.Web/StoreDb.svc")
        Dim client As New StoreDbClient(New BasicHttpBinding(), address)

        AddHandler client.GetCategoriesWithProductsCompleted, AddressOf client_GetCategoriesWithProductsCompleted
        client.GetCategoriesWithProductsAsync()
    End Sub

    Private Sub client_GetCategoriesWithProductsCompleted(ByVal sender As Object, ByVal e As GetCategoriesWithProductsCompletedEventArgs)
        Try
            lstCategories.ItemsSource = e.Result
        Catch err As Exception
            lblError.Text = "Failed to contact service."
        End Try
    End Sub

    Private Sub lstCategories_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
        lstProducts.ItemsSource = (CType(lstCategories.SelectedItem, Category)).Products
    End Sub
End Class
