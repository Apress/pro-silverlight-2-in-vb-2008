Imports System.Windows.Browser

Public Partial Class Page2
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        App.Navigate(New Page())
    End Sub

    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim element As HtmlElement = HtmlPage.Document.GetElementById("silverlightControl")
        element.SetStyleAttribute("width", Me.Width & "px")
        element.SetStyleAttribute("height", Me.Height & "px")
    End Sub
End Class
