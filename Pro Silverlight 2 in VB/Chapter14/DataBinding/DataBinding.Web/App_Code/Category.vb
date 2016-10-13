Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract()> _
Public Class Category
    Implements INotifyPropertyChanged
    Private _categoryName As String
    <DataMember()> _
    Public Property CategoryName() As String
        Get
            Return _categoryName
        End Get
        Set(ByVal value As String)
            _categoryName = value
            OnPropertyChanged(New PropertyChangedEventArgs("CategoryName"))
        End Set
    End Property

    Private _products As List(Of Product)
    <DataMember()> _
    Public Property Products() As List(Of Product)
        Get
            Return _products
        End Get
        Set(ByVal value As List(Of Product))
            _products = value
            OnPropertyChanged(New PropertyChangedEventArgs("Products"))
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
        If Not PropertyChangedEvent Is Nothing Then
            RaiseEvent PropertyChanged(Me, e)
        End If
    End Sub

    Public Sub New(ByVal categoryName As String, ByVal products As List(Of Product))
        Me.CategoryName = categoryName
        Me.Products = products
    End Sub

    Public Sub New()
    End Sub
End Class
