Imports System.IO

Public Partial Class OpenFile
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdOpenFile_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim openDialog As New OpenFileDialog()
        openDialog.Filter = "Text Files (*.txt)|*.txt"
        If openDialog.ShowDialog() = True Then
            Using reader As StreamReader = openDialog.File.OpenText()
                lblData.Text = reader.ReadToEnd()
            End Using
        End If
    End Sub
End Class
