Imports System.Net.Sockets
Imports System.IO
Imports System.Net

Public Class PolicyServer
    Private policy As Byte()
    Private listener As TcpListener

    Public Sub New(ByVal policyFile As String)
        ' Load the policy file.
        Dim policyStream As New FileStream(policyFile, FileMode.Open)
        policy = New Byte(policyStream.Length - 1){}
        policyStream.Read(policy, 0, policy.Length)
        policyStream.Close()
    End Sub

    Public Sub Start()
        ' Create the listener.
        listener = New TcpListener(IPAddress.Any, 943)
        listener.Start()

        ' Wait for a connection.
        listener.BeginAcceptTcpClient(AddressOf OnAcceptTcpClient, Nothing)
    End Sub

    Public Sub OnAcceptTcpClient(ByVal ar As IAsyncResult)
        If isStopped Then
        Return
        End If

        Console.WriteLine("Received policy request.")

        ' Wait for the next connection.
        listener.BeginAcceptTcpClient(AddressOf OnAcceptTcpClient, Nothing)

        ' Handle this connection.
        Try
            Dim client As TcpClient = listener.EndAcceptTcpClient(ar)

            Dim policyConnection As New PolicyConnection(client, policy)
            policyConnection.HandleRequest()
        Catch err As Exception
            Console.WriteLine(err.Message)
        End Try
    End Sub

    Private isStopped As Boolean
    Public Sub [Stop]()
        isStopped = True

        Try
            listener.Stop()
        Catch err As Exception
            Console.WriteLine(err.Message)
        End Try
    End Sub
End Class
