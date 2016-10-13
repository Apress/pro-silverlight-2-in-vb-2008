 Partial Public Class App
    Inherits Application
    
    public Sub New()
        InitializeComponent()
    End Sub
    
    Private Sub Application_Startup(ByVal o As Object, ByVal e As StartupEventArgs) Handles Me.Startup
        ' Load the main control
        Me.RootVisual = rootGrid
        rootGrid.Children.Add(New Page1())
    End Sub
    
    Private Sub Application_Exit(ByVal o As Object, ByVal e As EventArgs) Handles Me.Exit

    End Sub
    
    Private Sub Application_UnhandledException(ByVal sender As object, ByVal e As ApplicationUnhandledExceptionEventArgs) Handles Me.UnhandledException

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

    Private rootGrid As New Grid()
    Private pager As New Pager()

    Public Shared Sub Navigate(ByVal newPage As UserControl)
        Dim currentApp As App = CType(Application.Current, App)

        Dim type As Type = newPage.GetType()

        ' Record the page the application is 

        currentApp.pager.AddHistoryItem(type.Name)

        ' Change the currently displayed page.
        currentApp.rootGrid.Children.RemoveAt(0)
        currentApp.rootGrid.Children.Insert(0, newPage)
    End Sub

    Public Shared Sub RestorePage(ByVal pageClassName As String)
        Dim currentApp As App = CType(Application.Current, App)

        Dim type As Type = currentApp.GetType()
        Dim [assembly] As System.Reflection.Assembly = type.Assembly
        Dim newPage As UserControl = CType([assembly].CreateInstance(type.Namespace & "." & pageClassName), UserControl)

        ' Change the currently displayed page.
        currentApp.rootGrid.Children.RemoveAt(0)
        currentApp.rootGrid.Children.Insert(0, newPage)
    End Sub
    
End Class
