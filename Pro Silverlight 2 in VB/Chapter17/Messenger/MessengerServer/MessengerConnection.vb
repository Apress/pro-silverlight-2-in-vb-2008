Imports System.Net.Sockets
Imports System.IO

Public Class MessengerConnection
    Private client As TcpClient
    Private ID As String
    Private server As MessengerServer

    Public Sub New(ByVal client As TcpClient, ByVal ID As String, ByVal server As MessengerServer)
        Me.client = client
        Me.ID = ID
        Me.server = server
    End Sub

    Private message As Byte() = New Byte(1023){}

    Public Sub Start()
        Try
            ' Listen for messages.
            client.Client.BeginReceive(message, 0, message.Length, SocketFlags.None, New AsyncCallback(AddressOf OnDataReceived), Nothing)
        Catch se As SocketException
            Console.WriteLine(se.Message)
        End Try
    End Sub

    Public Sub OnDataReceived(ByVal asyn As IAsyncResult)
        Try
            Dim bytesRead As Integer = client.Client.EndReceive(asyn)

            If bytesRead > 0 Then
                ' Ask the server to send the message to all the clients.
                server.DeliverMessage(message, bytesRead)

                ' Listen for more messages.
                client.Client.BeginReceive(message, 0, message.Length, SocketFlags.None, New AsyncCallback(AddressOf OnDataReceived), Nothing)
            End If
        Catch err As Exception
            Console.WriteLine(err.Message)
        End Try
    End Sub

    Public Sub Close()
        Try
            client.Close()
        Catch err As Exception
            Console.WriteLine(err.Message)
        End Try
    End Sub

    Public Sub ReceiveMessage(ByVal data As Byte(), ByVal bytesRead As Integer)
        client.GetStream().Write(data, 0, bytesRead)
    End Sub
End Class
