Imports System.Windows.Browser

Public Partial Class ManipulateHtmlElement
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim element As HtmlElement = HtmlPage.Document.CreateElement("p")

        element.Id = "paragraph"
        element.SetProperty("innerHTML", "This is a new element. Click to change its background color.")

        HtmlPage.Document.Body.AppendChild(element)
        'HtmlPage.Document.Body.AppendChild(element, HtmlPage.Document.Body.Children[0]);

        'element.CssClass = "highlightedParagraph";            

        element.SetStyleAttribute("border", "solid 1px black")
        element.SetStyleAttribute("margin", "10px")
        element.SetStyleAttribute("padding", "10px")

        element.AttachEvent("onclick", AddressOf paragraph_Click)
    End Sub

    Private Sub cmdChange_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim element As HtmlElement = HtmlPage.Document.GetElementById("paragraph")
        If element Is Nothing Then
        Return
        End If

        element.SetProperty("innerHTML", "This HTML paragraph has been updated by Silverlight.")
        element.SetStyleAttribute("background", "white")
    End Sub

    Private Sub paragraph_Click(ByVal sender As Object, ByVal e As HtmlEventArgs)
        Dim element As HtmlElement = CType(sender, HtmlElement)
        element.SetProperty("innerHTML", "You clicked this HTML element, and Silverlight handled it.")
        element.SetStyleAttribute("background", "#00ff00")
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim element As HtmlElement = HtmlPage.Document.GetElementById("paragraph")
        If element Is Nothing Then
        Return
        End If

        element.Parent.RemoveChild(element)
    End Sub

    Private Sub cmdScript_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim element As HtmlElement = HtmlPage.Document.GetElementById("paragraph")
        If element Is Nothing Then
        Return
        End If

        Dim script As ScriptObject = CType(HtmlPage.Window.GetProperty("changeParagraph"), ScriptObject)
        script.InvokeSelf("Changed through JavaScript.")
    End Sub
End Class
