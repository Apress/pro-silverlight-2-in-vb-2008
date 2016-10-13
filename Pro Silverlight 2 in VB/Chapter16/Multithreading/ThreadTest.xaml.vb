Partial Public Class ThreadTest
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private threadWrapper As FindPrimesThreadWrapper
    Private Sub cmdFind_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Disable the button and clear previous results.
        cmdFind.IsEnabled = False
        cmdCancel.IsEnabled = True
        gridPrimes.ItemsSource = Nothing
        lblStatus.Text = ""

        ' Get the search range.
        Dim fromNumber, toNumber As Integer
        If (Not Int32.TryParse(txtFrom.Text, fromNumber)) Then
            lblStatus.Text = "Invalid From value."
            Return
        End If
        If (Not Int32.TryParse(txtTo.Text, toNumber)) Then
            lblStatus.Text = "Invalid To value."
            Return
        End If

        ' Start the search for primes on another thread.
        threadWrapper = New FindPrimesThreadWrapper(fromNumber, toNumber)
        AddHandler threadWrapper.Completed, AddressOf threadWrapper_Completed
        AddHandler threadWrapper.Cancelled, AddressOf threadWrapper_Cancelled
        threadWrapper.Start()

        lblStatus.Text = "The search is in progress..."
    End Sub

    Private Sub threadWrapper_Cancelled(ByVal sender As Object, ByVal e As EventArgs)
        Me.Dispatcher.BeginInvoke(AddressOf UpdateDisplay)
    End Sub

    Private Sub UpdateDisplay()
        lblStatus.Text = "Search cancelled."
        cmdFind.IsEnabled = True
        cmdCancel.IsEnabled = False
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        threadWrapper.RequestCancel()
    End Sub

    ' Temporarily store the prime list here while the call is
    ' marshalled to the right thread.
    Private recentPrimeList As Integer()

    Private Sub threadWrapper_Completed(ByVal sender As Object, ByVal e As FindPrimesCompletedEventArgs)
        Dim thread As FindPrimesThreadWrapper = CType(sender, FindPrimesThreadWrapper)
        If thread.Status = StatusState.Completed Then
            recentPrimeList = e.PrimeList
        Else
            recentPrimeList = Nothing
        End If

        Me.Dispatcher.BeginInvoke(AddressOf DisplayPrimeList)
    End Sub

    Private Sub DisplayPrimeList()
        If recentPrimeList IsNot Nothing Then
            lblStatus.Text = "Found " & recentPrimeList.Length & " prime numbers."
            gridPrimes.ItemsSource = recentPrimeList
        End If

        cmdFind.IsEnabled = True
        cmdCancel.IsEnabled = False
    End Sub



End Class


