Imports System.Windows.Data
Imports System.Windows.Media.Imaging
Imports System.Windows.Browser
Imports System.IO

Public Class ImagePathConverter
    Implements IValueConverter

    Private _rootUri As String
    Public Property RootUri() As String
        Get
            Return _rootUri
        End Get
        Set(ByVal value As String)
            _rootUri = value
        End Set
    End Property

    Public Sub New()
        Dim uri As String = HtmlPage.Document.DocumentUri.ToString()
        RootUri = uri.Remove(uri.LastIndexOf("/"c), uri.Length - uri.LastIndexOf("/"c))
    End Sub

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object _
      Implements IValueConverter.Convert
        Dim imagePath As String = RootUri & "/" & CStr(value)

        ' Hack for GIFs.
        ' (The database expect GIF files, but Silverlight only supports PNG and JPEG.)
        imagePath = imagePath.ToLower().Replace(".gif", ".png")

        Return New BitmapImage(New Uri(imagePath))
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object _
      Implements IValueConverter.ConvertBack
        Throw New NotSupportedException()
    End Function

End Class

