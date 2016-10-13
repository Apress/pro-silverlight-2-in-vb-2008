Imports System.Windows.Browser

Public Partial Class DissectPage
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub UserControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Start processing the top-level <html> element.
        Dim element As HtmlElement = HtmlPage.Document.DocumentElement
        ProcessElement(element, 0)
    End Sub

    Private Sub ProcessElement(ByVal element As HtmlElement, ByVal indent As Integer)
        ' Ignore comments.
        If element.TagName = "!" Then
        Return
        End If

        ' Indent the element to help show different levels of nesting.
        lblElementTree.Text += New String(" "c, indent * 4)

        ' Display the tag name.
        lblElementTree.Text &= "<" & element.TagName

        ' Only show the id attribute if it's set.
        If element.Id <> "" Then
        lblElementTree.Text &= " id=""" & element.Id & """"
        End If
        lblElementTree.Text &= ">" & Constants.vbLf

        ' Process all the elements nested inside the current element.
        For Each childElement As HtmlElement In element.Children
            ProcessElement(childElement, indent + 1)
        Next
    End Sub
End Class
