Imports System.ComponentModel

Partial Public Class BackgroundWorkerTest
    Inherits UserControl
    Private backgroundWorker As New BackgroundWorker()

    Public Sub New()
        InitializeComponent()

        backgroundWorker.WorkerReportsProgress = True
        backgroundWorker.WorkerSupportsCancellation = True
        AddHandler backgroundWorker.DoWork, AddressOf backgroundWorker_DoWork
        AddHandler backgroundWorker.ProgressChanged, AddressOf backgroundWorker_ProgressChanged
        AddHandler backgroundWorker.RunWorkerCompleted, AddressOf backgroundWorker_RunWorkerCompleted
    End Sub

    Private Sub cmdFind_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Disable the button and clear previous results.
        cmdFind.IsEnabled = False
        cmdCancel.IsEnabled = True
        gridPrimes.ItemsSource = Nothing
        lblStatus.Text = ""

        ' Get the search range.
        Dim From, [to] As Integer
        If (Not Int32.TryParse(txtFrom.Text, From)) Then
            lblStatus.Text = "Invalid From value."
            Return
        End If
        If (Not Int32.TryParse(txtTo.Text, [to])) Then
            lblStatus.Text = "Invalid To value."
            Return
        End If

        ' Start the search for primes on another thread.
        Dim input As New FindPrimesInput(From, [to])
        backgroundWorker.RunWorkerAsync(input)
    End Sub

    Private Sub backgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        ' Get the input values.
        Dim input As FindPrimesInput = CType(e.Argument, FindPrimesInput)

        ' Start the search for primes and wait.
        Dim primes As Integer() = FindPrimesWorker.FindPrimes(input.FromNumber, input.ToNumber, backgroundWorker)

        If backgroundWorker.CancellationPending Then
            e.Cancel = True
            Return
        End If

        ' Return the result.
        e.Result = primes
    End Sub

    Private Sub backgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            lblStatus.Text = "Search cancelled."
        ElseIf Not e.Error Is Nothing Then
            ' An error was thrown by the DoWork event handler.
            lblStatus.Text = e.Error.Message
        Else
            Dim primes As Integer() = CType(e.Result, Integer())
            lblStatus.Text = "Found " & primes.Length & " prime numbers."
            gridPrimes.ItemsSource = primes
        End If

        cmdFind.IsEnabled = True
        cmdCancel.IsEnabled = False
        progressBar.Width = 0
    End Sub

    Private maxWidth As Double
    Private Sub UserControl_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
        maxWidth = progressBarBackground.ActualWidth
    End Sub

    Private Sub backgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        progressBar.Width = CDbl(e.ProgressPercentage) / 100 * maxWidth
        lblProgress.Text = (CDbl(e.ProgressPercentage) / 100).ToString("P0")

        If Not e.UserState Is Nothing Then
            lblStatus.Text = "Found prime: " & e.UserState.ToString() & "..."
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        backgroundWorker.CancelAsync()
    End Sub


End Class
