Imports DataBinding.Local

Public Partial Class BindProductObject
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim product As New Product("AEFS100", "Portable Defibrillator", 77, "Analyzes the electrical activity of a person's heart and applies an electric shock if necessary.")
        gridProductDetails.DataContext = product
    End Sub

    Private Sub cmdChange_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim product As Product = CType(gridProductDetails.DataContext, Product)
        product.UnitCost *= 1.1
    End Sub

    Private Sub cmdCheck_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim product As Product = CType(gridProductDetails.DataContext, Product)
        lblInfo.Text = "Model Name: " & product.ModelName + Constants.vbLf & "Model Number: " & product.ModelNumber + Constants.vbLf & "Unit Cost: " & product.UnitCost
    End Sub

    Private Sub Grid_BindingValidationError(ByVal sender As Object, ByVal e As ValidationErrorEventArgs)
        lblInfo.Text = e.Error.Exception.Message
        lblInfo.Text += Constants.vbLf & "The stored value is still: " & (CType(gridProductDetails.DataContext, Product)).UnitCost.ToString()
        txtUnitCost.Focus()
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As TextChangedEventArgs)
        lblInfo.Text = ""
    End Sub
End Class
