Imports System.Net.Sockets
Imports System.Threading
Imports System.Net

Public Class MessengerServer
    Private listener As TcpListener
    Private clientNum As Integer
    Private clients As List(Of MessengerConnection) = New List(Of MessengerConnection)()

    Public Sub Start()
        ' The allowed port range is 4502-4532.
        listener = New TcpListener(IPAddress.Any, 4530)
        listener.Start()

        ' Wait for a connection request, 
        ' and return a TcpClient initialized for communication. 
        ' You could add code here to limit the maximum number of
        ' concurrent connections.
        listener.BeginAcceptTcpClient(AddressOf OnAcceptTcpClient, Nothing)
    End Sub

    Private Sub OnAcceptTcpClient(ByVal ar As IAsyncResult)
        If isStopped Then
        Return
        End If

        ' Listen for the next client.
        listener.BeginAcceptTcpClient(AddressOf OnAcceptTcpClient, Nothing)

        clientNum += 1
        Console.WriteLine("Messenger client #" & clientNum.ToString() & " connected.")
        Dim client As TcpClient = listener.EndAcceptTcpClient(ar)

        ' Create a new object to handle this connection.            
        Dim clientHandler As New MessengerConnection(client, "Client " & clientNum.ToString(), Me)
        clientHandler.Start()

        SyncLock clients
            clients.Add(clientHandler)
        End SyncLock
    End Sub

    Private isStopped As Boolean
    Public Sub [Stop]()
        isStopped = True
        If Not listener Is Nothing Then
            Try
                listener.Server.Close()
            Catch err As Exception
                Console.WriteLine(err.Message)
            End Try
        End If

        For Each client As MessengerConnection In clients
            client.Close()
        Next
    End Sub

    Public Sub DeliverMessage(ByVal message As Byte(), ByVal bytesRead As Integer)
        Console.WriteLine("Delivering message.")

        ' Duplicate the collection to prevent threading issues.
        Dim connectedClients As MessengerConnection()
        SyncLock clients
            connectedClients = clients.ToArray()
        End SyncLock

        For Each client As MessengerConnection In connectedClients
            Try
                client.ReceiveMessage(message, bytesRead)
            Catch
                ' Client is disconnected.
                ' Remove the client to avoid future attempts.
                SyncLock clients
                    clients.Remove(client)
                End SyncLock

                client.Close()
            End Try
        Next
    End Sub

End Class
