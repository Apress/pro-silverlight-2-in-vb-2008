Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Configuration

<ServiceContract(Namespace:=""), AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
Public Class StoreDb
    Private ds As New DataSet()

    Public Sub New()
        ds.ReadXmlSchema(HttpContext.Current.Server.MapPath("store.xsd"))
        ds.ReadXml(HttpContext.Current.Server.MapPath("store.xml"))
    End Sub

    <OperationContract()> _
    Public Function GetProduct(ByVal ID As Integer) As Product
        Dim productRow As DataRow = ds.Tables("Products").Select("ProductID = " & ID.ToString())(0)
        Dim product As New Product(CStr(productRow("ModelNumber")), CStr(productRow("ModelName")), Convert.ToDouble(productRow("UnitCost")), CStr(productRow("Description")), CStr(productRow("CategoryName")), CStr(productRow("ProductImage")))
        Return product
    End Function

    <OperationContract()> _
    Public Function GetProducts() As List(Of Product)
        Dim products As List(Of Product) = New List(Of Product)()
        For Each productRow As DataRow In ds.Tables("Products").Rows
            products.Add(New Product(CStr(productRow("ModelNumber")), CStr(productRow("ModelName")), Convert.ToDouble(productRow("UnitCost")), CStr(productRow("Description")), CStr(productRow("CategoryName")), CStr(productRow("ProductImage"))))
        Next
        Return products
    End Function

    <OperationContract()> _
    Public Function GetCategoriesWithProducts() As List(Of Category)
        Dim relCategoryProduct As DataRelation = ds.Relations(0)

        Dim categories As List(Of Category) = New List(Of Category)()
        For Each categoryRow As DataRow In ds.Tables("Categories").Rows
            Dim products As List(Of Product) = New List(Of Product)()
            For Each productRow As DataRow In categoryRow.GetChildRows(relCategoryProduct)
                products.Add(New Product(productRow("ModelNumber").ToString(), productRow("ModelName").ToString(), Convert.ToDouble(productRow("UnitCost")), productRow("Description").ToString()))
            Next
            categories.Add(New Category(categoryRow("CategoryName").ToString(), products))
        Next
        Return categories
    End Function
End Class
