Imports System.ServiceModel
Imports System.Windows.Browser
Imports DataBinding.DataService
Imports System.Globalization
Imports System.Collections.ObjectModel

Public Partial Class DataGridTest
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim address As New EndpointAddress("http://localhost:" & HtmlPage.Document.DocumentUri.Port & "/DataBinding.Web/StoreDb.svc")
        Dim client As New StoreDbClient(New BasicHttpBinding(), address)

        AddHandler client.GetProductsCompleted, AddressOf client_GetProductsCompleted
        client.GetProductsAsync()
    End Sub

    Private Sub client_GetProductsCompleted(ByVal sender As Object, ByVal e As GetProductsCompletedEventArgs)
        Try
            gridProducts.ItemsSource = e.Result
        Catch err As Exception
            lblInfo.Text = "Failed to contact service."
        End Try
    End Sub

    ' Reuse brush objects for efficiency in large data displays.
    Private highlightBrush As New SolidColorBrush(Colors.Orange)
    Private normalBrush As New SolidColorBrush(Colors.White)

    Private Sub gridProducts_LoadingRow(ByVal sender As Object, ByVal e As DataGridRowEventArgs)
        Dim product As Product = CType(e.Row.DataContext, Product)
        If product.UnitCost > 100 Then
            e.Row.Background = highlightBrush
        Else
            e.Row.Background = normalBrush
        End If

    End Sub

    Private Sub FormatRow(ByVal row As DataGridRow)
        Dim product As Product = CType(row.DataContext, Product)
        If product.UnitCost > 100 Then
            row.Background = highlightBrush
        Else
            row.Background = normalBrush
        End If

    End Sub

    Private Sub gridProducts_PreparingCellForEdit(ByVal sender As Object, ByVal e As DataGridPreparingCellForEditEventArgs)
        If e.Column.Header.ToString() = "Category" Then
            Dim categories As List(Of String) = New List(Of String)()
            categories.Add("Munitions")
            categories.Add("Travel")
            'this.Resources["CategoryList"] = categories;
            CType(e.EditingElement, ListBox).ItemsSource = categories
        End If
    End Sub

End Class
