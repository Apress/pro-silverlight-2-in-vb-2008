Imports System.ComponentModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class Product
    Implements INotifyPropertyChanged
    Private _hasChanges As Boolean = False
    Public Property HasChanges() As Boolean
        Get
            Return _hasChanges
        End Get
        Set(ByVal value As Boolean)
            _hasChanges = value
        End Set
    End Property

    Private _modelNumber As String
    <DataMember()> _
    Public Property ModelNumber() As String
        Get
            Return _modelNumber
        End Get
        Set(ByVal value As String)
            _modelNumber = value
            OnPropertyChanged(New PropertyChangedEventArgs("ModelNumber"))
        End Set
    End Property

    Private _modelName As String
    <DataMember()> _
    Public Property ModelName() As String
        Get
            Return _modelName
        End Get
        Set(ByVal value As String)
            _modelName = value
            OnPropertyChanged(New PropertyChangedEventArgs("ModelName"))
        End Set
    End Property

    Private _unitCost As Double
    <DataMember()> _
    Public Property UnitCost() As Double
        Get
            Return _unitCost
        End Get
        Set(ByVal value As Double)
            _unitCost = value
            OnPropertyChanged(New PropertyChangedEventArgs("UnitCost"))
        End Set
    End Property

    Private _description As String
    <DataMember()> _
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
            OnPropertyChanged(New PropertyChangedEventArgs("Description"))
        End Set
    End Property

    Private _categoryName As String
    <DataMember()> _
    Public Property CategoryName() As String
        Get
            Return _categoryName
        End Get
        Set(ByVal value As String)
            _categoryName = value
        End Set
    End Property

    Private _productImagePath As String
    <DataMember()> _
    Public Property ProductImagePath() As String
        Get
            Return _productImagePath
        End Get
        Set(ByVal value As String)
            _productImagePath = value
        End Set
    End Property

    Public Sub New(ByVal modelNumber As String, ByVal modelName As String, ByVal unitCost As Double, ByVal description As String)
        Me.ModelNumber = modelNumber
        Me.ModelName = modelName
        Me.UnitCost = unitCost
        Me.Description = description
    End Sub

    Public Sub New(ByVal modelNumber As String, ByVal modelName As String, ByVal unitCost As Double, ByVal description As String, ByVal productImagePath As String)
        Me.New(modelNumber, modelName, unitCost, description)
        Me.ProductImagePath = productImagePath
    End Sub

    Public Sub New(ByVal modelNumber As String, ByVal modelName As String, ByVal unitCost As Double, ByVal description As String, ByVal categoryName As String, ByVal productImagePath As String)
        Me.New(modelNumber, modelName, unitCost, description)
        Me.CategoryName = categoryName
        Me.ProductImagePath = productImagePath
    End Sub

    Public Sub New()
    End Sub

    Public Overrides Function ToString() As String
        Return ModelName & " (" & ModelNumber & ")"
    End Function

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
        If Not PropertyChangedEvent Is Nothing Then
            RaiseEvent PropertyChanged(Me, e)
        End If
    End Sub
End Class

