Imports System.Windows.Browser

<ScriptableType()> _
Partial Public Class ScriptableSilverlight
    Inherits UserControl
    Public Sub New()
        InitializeComponent()

        HtmlPage.RegisterScriptableObject("Page", Me)
        HtmlPage.RegisterCreateableType("RandomNumbers", GetType(RandomNumbers))
    End Sub

    <ScriptableMember()> _
    Public Sub ChangeText(ByVal newText As String)
        lbl.Text = newText
    End Sub

End Class

<ScriptableType()> _
Public Class RandomNumbers
    Private random As New Random()

    <ScriptableMember()> _
    Public Function GetRandomNumberInRange(ByVal from As Integer, ByVal [to] As Integer) As Integer
        Return random.Next(From, [to]+1)
    End Function
End Class
