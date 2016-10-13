Imports System.Threading

Public Enum StatusState
    Unstarted
    InProgress
    Completed
    Cancelled
End Enum

Public MustInherit Class ThreadWrapperBase
    ' This is the thread where the task is carried out.
    Private thread As Thread

    ' Track the status of the task.
    Private _status As StatusState = StatusState.Unstarted
    Public ReadOnly Property Status() As StatusState
        Get
            Return _status
        End Get
    End Property

    ' (You could add properties to return the thread ID and name here.)

    ' Start the new operation.
    Public Sub Start()
        If _status = StatusState.InProgress Then
            Throw New InvalidOperationException("Already in progress.")
        Else
            ' Initialize the new task.
            _status = StatusState.InProgress

            ' Create the thread and run it in the background,
            ' so it will terminate automatically if the application ends.
            thread = New Thread(AddressOf StartTaskAsync)
            thread.IsBackground = True

            ' Start the thread.
            thread.Start()
        End If
    End Sub

    Private Sub StartTaskAsync()
        DoTask()
        If CancelRequested Then
            _status = StatusState.Cancelled
            OnCancelled()
        Else
            _status = StatusState.Completed
            OnCompleted()
        End If
    End Sub

    ' Override this class to supply the task logic.
    Protected MustOverride Sub DoTask()

    ' Override this class to supply the callback logic.
    Protected MustOverride Sub OnCompleted()

    Public Event Cancelled As EventHandler
    Protected Sub OnCancelled()
        If Not CancelledEvent Is Nothing Then
            RaiseEvent Cancelled(Me, EventArgs.Empty)
        End If
    End Sub

    ' Flag that indicates a stop is requested.
    Private _cancelRequested As Boolean = False
    Protected ReadOnly Property CancelRequested() As Boolean
        Get
            Return _cancelRequested
        End Get
    End Property

    Public Sub RequestCancel()
        _cancelRequested = True
    End Sub

End Class

