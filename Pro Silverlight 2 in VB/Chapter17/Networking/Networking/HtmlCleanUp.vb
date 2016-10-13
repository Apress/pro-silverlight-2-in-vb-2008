Imports System.Windows.Data
Imports System.Text.RegularExpressions
Imports System.Windows.Browser

Public Class HtmlCleanUpConverter
    Implements IValueConverter

    Private _maxCharacterLength As Integer = 200
    Public Property MaxCharacterLength() As Integer
        Get
            Return _maxCharacterLength
        End Get
        Set(ByVal value As Integer)
            _maxCharacterLength = value
        End Set
    End Property

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object _
      Implements IValueConverter.Convert

        ' Remove tags, newlines, and spaces.
        Dim returnString As String = Regex.Replace(TryCast(value, String), "<.*?>", "")
        returnString = Regex.Replace(returnString, "\n+\s+", Constants.vbLf + Constants.vbLf)

        ' Decode HTML entities.
        returnString = HttpUtility.HtmlDecode(returnString)

        ' Shorten.
        If returnString.Length > MaxCharacterLength Then
            Return returnString.Substring(0, MaxCharacterLength) & "..."
        Else
            Return returnString
        End If
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object _
      Implements IValueConverter.ConvertBack

        Throw New NotImplementedException()
    End Function
End Class

