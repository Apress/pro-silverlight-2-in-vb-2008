Imports System.Windows.Browser

Partial Public Class Page
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim target As HtmlElement = HtmlPage.Document.GetElementById("target")
        target.AttachEvent("onmouseover", AddressOf element_MouseOver)
    End Sub

    Private Sub element_MouseOver(ByVal sender As Object, ByVal e As HtmlEventArgs)
        Dim target As HtmlElement = HtmlPage.Document.GetElementById("target")
        Dim targetLeft As Double = Convert.ToDouble(target.GetProperty("offsetLeft")) - 20
        Dim targetTop As Double = Convert.ToDouble(target.GetProperty("offsetTop")) - 20

        Dim silverlightControl As HtmlElement = HtmlPage.Document.GetElementById("silverlightControlHost")
        silverlightControl.SetStyleAttribute("left", targetLeft.ToString() & "px")
        silverlightControl.SetStyleAttribute("top", targetTop.ToString() & "px")
        silverlightControl.SetStyleAttribute("width", Me.Width & "px")
        silverlightControl.SetStyleAttribute("height", Me.Height & "px")

        fadeUp.Begin()

    End Sub

    Private Sub Page_MouseLeave(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim silverlightControl As HtmlElement = HtmlPage.Document.GetElementById("silverlightControlHost")
        silverlightControl.SetStyleAttribute("width", "0px")
        silverlightControl.SetStyleAttribute("height", "0px")
    End Sub

End Class
