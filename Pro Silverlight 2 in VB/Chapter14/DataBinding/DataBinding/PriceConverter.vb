Imports System.Windows.Data
Imports System.Globalization

Public Class PriceConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object _
      Implements System.Windows.Data.IValueConverter.Convert
        Dim price As Double = CDbl(value)
        Return price.ToString("C", culture)
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object _
      Implements System.Windows.Data.IValueConverter.ConvertBack
        Dim price As String = value.ToString()

        Dim result As Double
        If Double.TryParse(price, NumberStyles.Any, culture, result) Then
            Return result
        End If
        Return value
    End Function

End Class

