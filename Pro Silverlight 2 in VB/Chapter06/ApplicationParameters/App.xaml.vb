Public Enum ViewMode
    Customer
    Employee
End Enum

Partial Public Class App
    Inherits Application

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Application_Startup(ByVal o As Object, ByVal e As StartupEventArgs) Handles Me.Startup
        ' Take the view mode setting, and store in an application property.
        If e.InitParams.ContainsKey("viewMode") Then
            Dim view As String = e.InitParams("viewMode")
            If view = "Employee" Then
                _viewMode = ViewMode.Employee
            End If
        End If

        ' Create the root page.
        Me.RootVisual = New Page()
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

    Private _viewMode As ViewMode = ViewMode.Customer
    Public ReadOnly Property ViewMode() As ViewMode
        Get
            Return _viewMode
        End Get
    End Property

End Class
