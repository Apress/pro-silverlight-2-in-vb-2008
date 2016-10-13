<%@ WebHandler Language="vb" Class="PopulationRestService" %>

Imports System
Imports System.Web

Public Class PopulationRestService
    Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim year As String = context.Request.Form("year")
        year = year.Replace(",", "")
        year = year.Trim()

        Dim isBc As Boolean = False
        If year.EndsWith("BC", StringComparison.OrdinalIgnoreCase) Then
            isBc = True
            year = year.Remove(year.IndexOf("BC", StringComparison.OrdinalIgnoreCase))
            year = year.Trim()
        End If
        Dim yearNumber As Integer = Int32.Parse(year)

        context.Response.ContentType = "text/plain"
        context.Response.Write(GetPopulation(yearNumber, isBc))
    End Sub
    Private Function GetPopulation(ByVal year As Integer, ByVal isBc As Boolean) As Integer
        If isBc Then
            If year >= 70000 Then
            Return 2
            ElseIf year >= 10000 Then
            Return 1000
            ElseIf year >= 9000 Then
            Return 3000
            ElseIf year >= 8000 Then
            Return 5000
            ElseIf year >= 7000 Then
            Return 7000
            ElseIf year >= 6000 Then
            Return 10000
            ElseIf year >= 5000 Then
            Return 15000
            ElseIf year >= 4000 Then
            Return 20000
            ElseIf year >= 3000 Then
            Return 25000
            ElseIf year >= 2000 Then
            Return 25000
            ElseIf year >= 1000 Then
            Return 50000
            ElseIf year >= 500 Then
            Return 100000
            Else
                Return 200000
            End If
        Else
            If year < 1000 Then
            Return 200000
            ElseIf year < 1750 Then
            Return 310000
            ElseIf year < 1800 Then
            Return 790000
            ElseIf year < 1850 Then
            Return 978000
            ElseIf year < 1900 Then
            Return 1262000
            ElseIf year < 1950 Then
            Return 1650000
            ElseIf year < 1955 Then
            Return 2519000
            ElseIf year < 1960 Then
            Return 2756000
            ElseIf year < 1965 Then
            Return 2982000
            ElseIf year < 1970 Then
            Return 3349000
            ElseIf year < 1975 Then
            Return 3692000
            ElseIf year < 1980 Then
            Return 4068000
            ElseIf year < 1985 Then
            Return 4435000
            ElseIf year < 1990 Then
            Return 4831000
            ElseIf year < 1995 Then
            Return 5264000
            ElseIf year < 2000 Then
            Return 5764000
            ElseIf year < 2005 Then
            Return 6071000
            Else
                Return 6452000
            End If
        End If
    End Function

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class