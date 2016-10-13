Public Class FindPrimesThreadWrapper
    Inherits ThreadWrapperBase
    Public Event Completed As EventHandler(Of FindPrimesCompletedEventArgs)

    ' Store the input and output information.
    Private fromNumber, toNumber As Integer
    Private primeList As Integer()

    Public Sub New(ByVal fromNumber As Integer, ByVal toNumber As Integer)
        Me.fromNumber = fromNumber
        Me.toNumber = toNumber
    End Sub

    Protected Overrides Sub DoTask()
        Dim list As Integer() = New Integer(toNumber - fromNumber - 1) {}

        ' Create an array containing all integers between the two specified numbers.
        For i As Integer = 0 To list.Length - 1
            list(i) = fromNumber
            fromNumber += 1
        Next


        'find out the module for each item in list, divided by each d, where
        'd is < or == to sqrt(to)
        'if the remainder is 0, the nubmer is a composite, and thus
        'we mark its position with 0 in the marks array,
        'otherwise the number is a prime, and thus mark it with 1
        Dim maxDiv As Integer = CInt(Fix(Math.Floor(Math.Sqrt(toNumber))))

        Dim mark As Integer() = New Integer(list.Length - 1) {}

        Dim lastPrime As Integer

        For i As Integer = 0 To list.Length - 1
            For j As Integer = 2 To maxDiv

                If (list(i) <> j) AndAlso (list(i) Mod j = 0) Then
                    mark(i) = 1
                    lastPrime = list(i)
                End If

            Next


            Dim iteration As Integer = list.Length / 100
            If i Mod iteration = 0 Then
                If CancelRequested Then
                    ' Return without doing any more work.
                    Return
                End If
            End If

        Next

        'create new array that contains only the primes, and return that array
        Dim primes As Integer = 0
        For i As Integer = 0 To mark.Length - 1
            If mark(i) = 0 Then
                primes += 1
            End If

        Next

        Dim ret As Integer() = New Integer(primes - 1) {}
        Dim curs As Integer = 0
        For i As Integer = 0 To mark.Length - 1
            If mark(i) = 0 Then
                ret(curs) = list(i)
                curs += 1
            End If
        Next

        primeList = ret
        OnCompleted()
    End Sub

    Protected Overrides Sub OnCompleted()
        ' Signal that the operation is complete.
        If Not CompletedEvent Is Nothing Then
            RaiseEvent Completed(Me, New FindPrimesCompletedEventArgs(fromNumber, toNumber, primeList))
        End If
    End Sub

    ' Allow access to the result once the task is finished.
    Public Function GetResultOfLastTask() As Integer()
        If Status = StatusState.Completed Then
            Return primeList
        Else
            Return Nothing
        End If
    End Function
End Class


Public Class FindPrimesCompletedEventArgs
    Inherits EventArgs
    Private primeList_Renamed As Integer()
    Public Property PrimeList() As Integer()
        Get
            Return primeList_Renamed
        End Get
        Set(ByVal value As Integer())
            primeList_Renamed = value
        End Set
    End Property

    Private _fromNumber As Integer
    Public Property FromNumber() As Integer
        Get
            Return _fromNumber
        End Get
        Set(ByVal value As Integer)
            _fromNumber = value
        End Set
    End Property

    Private _toNumber As Integer
    Public Property ToNumber() As Integer
        Get
            Return _toNumber
        End Get
        Set(ByVal value As Integer)
            _toNumber = value
        End Set
    End Property

    Public Sub New(ByVal fromNumber As Integer, ByVal toNumber As Integer, ByVal primeList As Integer())
        Me.FromNumber = fromNumber
        Me.ToNumber = toNumber
        Me.PrimeList = primeList
    End Sub
End Class
