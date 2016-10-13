Imports System.ServiceModel
Imports System.Windows.Browser
Imports DataBinding.DataService

Public Partial Class FilterCollectionWithLinq
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private products As List(Of Product) = New List(Of Product)()


    Private minCost As Double
    Private Sub cmdGetProducts_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        If (Not Double.TryParse(txtMinimumCost.Text, minCost)) Then
            lblError.Text = "The minimum cost is not a valid number."
            Return
        End If

        Dim address As New EndpointAddress("http://localhost:" & HtmlPage.Document.DocumentUri.Port & "/DataBinding.Web/StoreDb.svc")
        Dim client As New StoreDbClient(New BasicHttpBinding(), address)

        AddHandler client.GetProductsCompleted, AddressOf client_GetProductsCompleted
        client.GetProductsAsync()
    End Sub

    Private Sub client_GetProductsCompleted(ByVal sender As Object, ByVal e As GetProductsCompletedEventArgs)
        Try
            products.Clear()
            Dim matches As IEnumerable(Of Product) = From product In e.Result Where product.UnitCost >= minCost Select product

            lstProducts.ItemsSource = matches
        Catch err As Exception
            lblError.Text = "Failed to contact service."
        End Try
    End Sub



    Private Sub lstProducts_SelectionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
        gridProductDetails.DataContext = lstProducts.SelectedItem
    End Sub

End Class
