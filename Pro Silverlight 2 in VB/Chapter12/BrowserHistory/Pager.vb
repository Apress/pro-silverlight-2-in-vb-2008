Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.Windows.Browser

<ScriptableType> _
Public Class Pager
    Public Sub AddHistoryItem(ByVal stateKey As String)
        AddHistoryItem(stateKey, "Pager")
    End Sub

    Private pageSwitch As Boolean = False

    Public Sub AddHistoryItem(ByVal stateKey As String, ByVal pagerElementName As String)
        currentStateKey = stateKey
        Dim iframe As HtmlElement = HtmlPage.Document.GetElementById(pagerElementName)
        pageSwitch = Not pageSwitch

        If pageSwitch Then
            iframe.SetAttribute("src", "Pager1.html?StateKey=" & stateKey)
        Else
            iframe.SetAttribute("src", "Pager2.html?StateKey=" & stateKey)
        End If

    End Sub


    Public Sub New()
        HtmlPage.RegisterScriptableObject("PagerScript", Me)
    End Sub

    Private currentStateKey As String

    <ScriptableMember> _
    Public Sub Navigate(ByVal stateKey As String)
        'DEBUG
        'TextBlock txt = (TextBlock)((Grid)App.Current.RootVisual).Children[1];
        'txt.Text = DateTime.Now.TimeOfDay.ToString() + " " + stateKey;


        If stateKey <> currentStateKey Then
            App.RestorePage(stateKey)
            pageSwitch = Not pageSwitch
            currentStateKey = stateKey
        End If

    End Sub

End Class
