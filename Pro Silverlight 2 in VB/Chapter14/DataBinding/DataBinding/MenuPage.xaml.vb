Imports System.Reflection

Public Partial Class MenuPage
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub lstPages_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
        ' Get the selected item.
        Dim newPageName As String = (CType(e.AddedItems(0), ListBoxItem)).Content.ToString()

        ' Create an instance of the page named
        ' by the current button.
        Dim type As Type = Me.GetType()
        Dim [assembly] As System.Reflection.Assembly = type.Assembly
        Dim newPage As UserControl = CType([assembly].CreateInstance(type.Namespace & "." & newPageName), UserControl)

        ' Show the page.
        pagePlaceholder.Child = newPage
    End Sub
End Class
