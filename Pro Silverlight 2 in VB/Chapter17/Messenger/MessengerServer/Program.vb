
Friend Class Program
    Shared Sub Main(ByVal args As String())
        Dim policyServer As New PolicyServer("clientaccesspolicy.xml")
        policyServer.Start()
        Console.WriteLine("Policy server started.")

        Dim messengerServer As New MessengerServer()
        messengerServer.Start()
        Console.WriteLine("Messenger server started.")

        Console.WriteLine("Press Enter to exit.")
        ' Wait for an enter key. You could also wait for a specific input
        ' string (like "quit") or a single key using Console.ReadKey().
        Console.ReadLine()

        policyServer.Stop()
        Console.WriteLine("Policy server shut down.")

        messengerServer.Stop()
        Console.WriteLine("Messenger server shut down.")
    End Sub
End Class
