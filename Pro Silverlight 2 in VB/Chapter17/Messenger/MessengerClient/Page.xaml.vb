Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Imports System.Xml.Serialization

Public Partial Class Page
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    ' The socket for the underlying connection.
    Private socket As Socket

    Private Sub cmdConnect_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Try
            If (socket IsNot Nothing) AndAlso (socket.Connected) Then
                socket.Close()
            End If
        Catch err As Exception
            AddMessage("ERROR: " & err.Message)
        End Try

        Dim endPoint As New DnsEndPoint(Application.Current.Host.Source.DnsSafeHost, 4530)
        socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        Dim args As New SocketAsyncEventArgs()
        args.UserToken = socket
        args.RemoteEndPoint = endPoint
        AddHandler args.Completed, AddressOf OnSocketConnectCompleted
        socket.ConnectAsync(args)
    End Sub

    Private Sub OnSocketConnectCompleted(ByVal sender As Object, ByVal e As SocketAsyncEventArgs)
        If Not socket.Connected Then
            AddMessage("Connection failed.")
            Return
        End If

        AddMessage("Connected to server.")

        ' Messages can be a maximum of 1024 bytes.
        Dim response(1023) As Byte
        e.SetBuffer(response, 0, response.Length)
        RemoveHandler e.Completed, AddressOf OnSocketConnectCompleted
        AddHandler e.Completed, AddressOf OnSocketReceive

        ' Listen for messages.
        socket.ReceiveAsync(e)
    End Sub

    Private Sub OnSocketReceive(ByVal sender As Object, ByVal e As SocketAsyncEventArgs)
        If e.BytesTransferred = 0 Then
            AddMessage("Server disconnected.")
            Try
                socket.Close()
            Catch
            End Try
            Return
        End If

        Try
            ' Retrieve and display the message.                
            Dim serializer As New XmlSerializer(GetType(Message))
            Dim ms As New MemoryStream()
            ms.Write(e.Buffer, 0, e.BytesTransferred)
            ms.Position = 0
            Dim message As Message = CType(serializer.Deserialize(ms), Message)

            AddMessage("[" & message.Sender & "] " & message.MessageText & " (at " & message.SendTime.ToLongTimeString() & ")")
        Catch err As Exception
            AddMessage("ERROR: " & err.Message)
        End Try

        ' Listen for more messages.
        socket.ReceiveAsync(e)
    End Sub

    Private Sub cmdSend_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        If (socket Is Nothing) OrElse (socket.Connected = False) Then
            AddMessage("ERROR: Not connected.")
            Return
        End If

        Dim args As New SocketAsyncEventArgs()

        ' Prepare the message.
        Dim serializer As New XmlSerializer(GetType(Message))
        Dim ms As New MemoryStream()
        serializer.Serialize(ms, New Message(txtMessage.Text, txtName.Text))
        Dim messageData As Byte() = ms.ToArray()
        ' (You could check the 1024 message limit here.)
        Dim bufferList As New List(Of ArraySegment(Of Byte))()
        bufferList.Add(New ArraySegment(Of Byte)(messageData))
        args.BufferList = bufferList

        ' Send the message.
        socket.SendAsync(args)
    End Sub

    ' Add to the label, making sure the code runs on the user interface thread.
    Private Sub AddMessage(ByVal message As String)

        If Me.CheckAccess() Then
            lblMessages.Text += message & Environment.NewLine
            scrollViewer.ScrollToVerticalOffset(scrollViewer.ScrollableHeight)
        Else
            ' Get on the right thread.
            Dispatcher.BeginInvoke(New Action(Of String)(AddressOf AddMessage), message)
        End If
    End Sub
End Class
