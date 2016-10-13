Imports System.ServiceModel
Imports System.ServiceModel.Activation

<ServiceContract(Namespace:=""), AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
Public Class TestService
    <OperationContract()> _
    Public Function GetServerTime() As DateTime
        Return DateTime.Now
    End Function

    <OperationContract()> _
    Public Function GetCachedServerTime() As DateTime
        ' Check the cache.
        Dim context As HttpContext = HttpContext.Current

        If Not context.Cache("CurrentDateTime") Is Nothing Then
            ' Retrieve it from the cache
            Return CDate(context.Cache("CurrentDateTime"))
        Else
            ' Regenerate it.
            Dim now As DateTime = DateTime.Now

            ' Now store it in the cache for 5 seconds.
            context.Cache.Insert("CurrentDateTime", now, Nothing, DateTime.Now.AddSeconds(5), TimeSpan.Zero)

            Return now
        End If
    End Function
End Class