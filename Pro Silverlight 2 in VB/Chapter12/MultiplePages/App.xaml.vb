Imports System.Windows.Browser

Partial Public Class App
    Inherits Application

    Public Sub New()
        InitializeComponent()
    End Sub

    ' This Grid will host your pages.
    Private rootGrid As New Grid()

    Private Sub Application_Startup(ByVal o As Object, ByVal e As StartupEventArgs) Handles Me.Startup
        Me.RootVisual = rootGrid

        If String.IsNullOrEmpty(HtmlPage.Window.CurrentBookmark) Then
            rootGrid.Children.Add(New Page())
        Else
            Try
                Dim type As Type = Me.GetType()
                Dim [assembly] As System.Reflection.Assembly = type.Assembly
                Dim newPage As UserControl = CType([assembly].CreateInstance(HtmlPage.Window.CurrentBookmark), UserControl)
                rootGrid.Children.Add(newPage)
            Catch
                rootGrid.Children.Add(New Page())
            End Try
        End If
    End Sub

    Private Sub Application_Exit(ByVal o As Object, ByVal e As EventArgs) Handles Me.Exit

    End Sub

    Private Sub Application_UnhandledException(ByVal sender As Object, ByVal e As ApplicationUnhandledExceptionEventArgs) Handles Me.UnhandledException

        ' If the app is running outside of the debugger then report the exception using
        ' the browser's exception mechanism. On IE this will display it a yellow alert 
        ' icon in the status bar and Firefox will display a script error.
        If Not System.Diagnostics.Debugger.IsAttached Then

            ' NOTE: This will allow the application to continue running after an exception has been thrown
            ' but not handled. 
            ' For production applications this error handling should be replaced with something that will 
            ' report the error to the website and stop the application.
            e.Handled = True

            Try
                Dim errorMsg As String = e.ExceptionObject.Message + e.ExceptionObject.StackTrace
                errorMsg = errorMsg.Replace(""""c, "\"c).Replace("\r\n", "\n")

                System.Windows.Browser.HtmlPage.Window.Eval("throw New Error(""Unhandled Error in Silverlight 2 Application " + errorMsg + """);")
            Catch

            End Try
        End If
    End Sub

    Public Shared Sub Navigate(ByVal newPage As UserControl)
        HtmlPage.Window.NavigateToBookmark(newPage.GetType().FullName)

        Dim currentApp As App = CType(Application.Current, App)

        ' Change the currently displayed page.
        currentApp.rootGrid.Children.Clear()
        currentApp.rootGrid.Children.Add(newPage)
    End Sub

End Class
