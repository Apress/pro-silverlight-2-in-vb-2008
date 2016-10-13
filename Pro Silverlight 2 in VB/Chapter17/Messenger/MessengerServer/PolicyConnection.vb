Imports System.Net.Sockets
Imports System.IO

Public Class PolicyConnection
    Private client As TcpClient
    Private policy As Byte()

    Public Sub New(ByVal client As TcpClient, ByVal policy As Byte())
        Me.client = client
        Me.policy = policy
    End Sub

    ' The request that the client sends.
    Private Shared policyRequestString As String = "<policy-file-request/>"

    Public Sub HandleRequest()
        Dim s As Stream = client.GetStream()

        ' Read the policy request string.
        Dim buffer As Byte() = New Byte(policyRequestString.Length - 1){}
        ' Only wait 5 seconds.
        client.ReceiveTimeout = 5000
        s.Read(buffer, 0, buffer.Length)

        ' Send the policy.
        s.Write(policy, 0, policy.Length)

        ' Close the connection.
        client.Close()

        Console.WriteLine("Served policy file.")
    End Sub
End Class
