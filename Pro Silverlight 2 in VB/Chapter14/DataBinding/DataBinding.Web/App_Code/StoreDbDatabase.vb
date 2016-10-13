'Imports System.ServiceModel
'Imports System.ServiceModel.Activation
'Imports System.Collections.Generic
'Imports System.Data.SqlClient
'Imports System.Data
'Imports System.Web.Configuration

'<ServiceContract(Namespace:=""), AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
'Public Class StoreDb
'    Private connectionString As String = WebConfigurationManager.ConnectionStrings("StoreDb").ConnectionString

'    <OperationContract()> _
'    Public Function GetProduct(ByVal ID As Integer) As Product
'        Dim con As New SqlConnection(connectionString)
'        Dim cmd As New SqlCommand("GetProductByID", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        cmd.Parameters.AddWithValue("@ProductID", ID)

'        Try
'            con.Open()
'            Dim reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleRow)
'            If reader.Read() Then
'                ' Create a Product object that wraps the 
'                ' current record.
'                Dim product As New Product(CStr(reader("ModelNumber")), CStr(reader("ModelName")), Convert.ToDouble(reader("UnitCost")), CStr(reader("Description")), CStr(reader("ProductImage")))
'                Return product
'            Else
'                Return Nothing
'            End If
'        Finally
'            con.Close()
'        End Try
'    End Function

'    <OperationContract()> _
'    Public Function GetProducts() As List(Of Product)
'        Dim con As New SqlConnection(connectionString)
'        Dim cmd As New SqlCommand("GetProducts", con)
'        cmd.CommandType = CommandType.StoredProcedure

'        Dim products As List(Of Product) = New List(Of Product)()
'        Try
'            con.Open()
'            Dim reader As SqlDataReader = cmd.ExecuteReader()
'            Do While reader.Read()
'                ' Create a Product object that wraps the
'                ' current record.
'                Dim product As New Product(CStr(reader("ModelNumber")), CStr(reader("ModelName")), Convert.ToDouble(reader("UnitCost")), CStr(reader("Description")), CStr(reader("CategoryName")), CStr(reader("ProductImage")))

'                ' Add to collection
'                products.Add(product)
'            Loop
'        Finally
'            con.Close()
'        End Try
'        Return products
'    End Function

'    <OperationContract()> _
'    Public Function GetCategoriesWithProducts() As List(Of Category)
'        Dim con As New SqlConnection(connectionString)
'        Dim cmd As New SqlCommand("GetProducts", con)
'        cmd.CommandType = CommandType.StoredProcedure
'        Dim adapter As New SqlDataAdapter(cmd)

'        Dim ds As New DataSet()
'        adapter.Fill(ds, "Products")
'        cmd.CommandText = "GetCategories"
'        adapter.Fill(ds, "Categories")

'        ' Set up a relation between these tables (optional).
'        Dim relCategoryProduct As New DataRelation("CategoryProduct", ds.Tables("Categories").Columns("CategoryID"), ds.Tables("Products").Columns("CategoryID"))
'        ds.Relations.Add(relCategoryProduct)

'        Dim categories As List(Of Category) = New List(Of Category)()
'        For Each categoryRow As DataRow In ds.Tables("Categories").Rows
'            Dim products As List(Of Product) = New List(Of Product)()
'            For Each productRow As DataRow In categoryRow.GetChildRows(relCategoryProduct)
'                products.Add(New Product(productRow("ModelNumber").ToString(), productRow("ModelName").ToString(), Convert.ToDouble(productRow("UnitCost")), productRow("Description").ToString()))
'            Next
'            categories.Add(New Category(categoryRow("CategoryName").ToString(), products))
'        Next
'        Return categories
'    End Function
'End Class
