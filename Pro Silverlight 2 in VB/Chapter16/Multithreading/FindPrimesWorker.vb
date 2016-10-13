Public Class FindPrimesWorker
    Public Shared Function FindPrimes(ByVal fromNumber As Integer, ByVal toNumber As Integer) As Integer()
        Return FindPrimes(fromNumber, toNumber, Nothing)
    End Function

    Public Shared Function FindPrimes(ByVal fromNumber As Integer, ByVal toNumber As Integer, ByVal backgroundWorker As System.ComponentModel.BackgroundWorker) As Integer()
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
            If (i Mod iteration = 0) AndAlso (Not backgroundWorker Is Nothing) Then
                If backgroundWorker.CancellationPending Then
                    ' Return without doing any more work.
                    Return Nothing
                End If

                If backgroundWorker.WorkerReportsProgress Then
                    'float progress = ((float)(i + 1)) / list.Length * 100;
                    backgroundWorker.ReportProgress(i \ iteration, i)
                    '(int)Math.Round(progress));
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

        If Not backgroundWorker Is Nothing AndAlso backgroundWorker.WorkerReportsProgress Then
            backgroundWorker.ReportProgress(100)
        End If

        Return ret

    End Function


End Class
