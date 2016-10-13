Imports System.IO.IsolatedStorage
Imports System.Xml.Serialization
Imports System.IO

Public Partial Class SerializeData
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    ' requires System.Xml.Serialization.dll refrence
    Private serializer As New XmlSerializer(GetType(Person))

    Private currentPerson As Person
    Private Sub lstPeople_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
        If lstPeople.SelectedItem Is Nothing Then
        Return
        End If

        Using store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using stream As FileStream = store.OpenFile(lstPeople.SelectedItem.ToString(), FileMode.Open)
                currentPerson = CType(serializer.Deserialize(stream), Person)
                txtFirstName.Text = currentPerson.FirstName
                txtLastName.Text = currentPerson.LastName
                dpDateOfBirth.SelectedDate = currentPerson.DateOfBirth
            End Using
        End Using
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Using store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            If Not currentPerson Is Nothing Then
                store.DeleteFile(currentPerson.FirstName + currentPerson.LastName & ".person")
            End If

            Dim person As New Person(txtFirstName.Text, txtLastName.Text, dpDateOfBirth.SelectedDate)
            Using stream As FileStream = store.CreateFile(person.FirstName + person.LastName & ".person")
                serializer.Serialize(stream, person)
            End Using
            lstPeople.ItemsSource = store.GetFileNames("*.person")

            currentPerson = Nothing
            txtFirstName.Text = ""
            txtLastName.Text = ""
        End Using
    End Sub

    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Using store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim people As String() = store.GetFileNames("*.person")
            lstPeople.ItemsSource = people
        End Using

    End Sub

    Private Sub Delete_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        If lstPeople.SelectedItem Is Nothing Then
        Return
        End If

        Using store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            store.DeleteFile(lstPeople.SelectedItem.ToString())
            lstPeople.ItemsSource = store.GetFileNames("*.person")

            currentPerson = Nothing
            txtFirstName.Text = ""
            txtLastName.Text = ""
        End Using
    End Sub
End Class

Public Class Person

    Private _firstName As String
    Public Property FirstName() As String
        Get
            Return _firstName
        End Get
        Set(ByVal value As String)
            _firstName = value
        End Set
    End Property

    Private _lastName As String
    Public Property LastName() As String
        Get
            Return _lastName
        End Get
        Set(ByVal value As String)
            _lastName = value
        End Set
    End Property

    Private _dateOfBirth As DateTime
    Public Property DateOfBirth() As DateTime
        Get
            Return _dateOfBirth
        End Get
        Set(ByVal value As DateTime)
            _dateOfBirth = value
        End Set
    End Property

    Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal dateOfBirth As Nullable(Of DateTime))
        Me.FirstName = firstName
        Me.LastName = lastName
        Me.DateOfBirth = dateOfBirth
    End Sub

    ' Required for serialization support.
    Public Sub New()
    End Sub
End Class
