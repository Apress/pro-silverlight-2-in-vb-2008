Imports System.IO.IsolatedStorage
Imports System.IO

Public Partial Class WriteDate
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdWrite_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Write to isolated storage.
        Try
            Dim store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()

            Using fs As IsolatedStorageFileStream = store.CreateFile("date.txt")
                Dim w As New StreamWriter(fs)
                w.Write(DateTime.Now)
                w.Close()
            End Using
            lblData.Text = "Data written to date.txt"
        Catch err As Exception
            lblData.Text = err.Message
        End Try
    End Sub

    Private Sub cmdRead_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Read from isolated storage.
        Try
            Dim store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()

            Using fs As IsolatedStorageFileStream = store.OpenFile("date.txt", FileMode.Open)
                Dim r As New StreamReader(fs)
                lblData.Text = r.ReadLine()
                r.Close()
            End Using
        Catch err As Exception
            ' An exception will occur if you attempt to open a file that doesn't exist.
            lblData.Text = err.Message
        End Try
    End Sub

    Private Sub cmdTryIncrease_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        lblData.Text = "Current quota: " & store.Quota.ToString()
        If store.IncreaseQuotaTo(store.Quota + 10) Then
            lblData.Text += Environment.NewLine & "Increased to: " & store.Quota.ToString()
        End If

    End Sub


End Class

