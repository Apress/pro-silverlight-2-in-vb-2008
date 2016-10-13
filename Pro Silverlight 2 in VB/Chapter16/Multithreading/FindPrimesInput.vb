Public Class FindPrimesInput
    Private _toNumber As Integer
    Public Property ToNumber() As Integer
        Get
            Return _toNumber
        End Get
        Set(ByVal value As Integer)
            _toNumber = value
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

    Public Sub New(ByVal fromNumber As Integer, ByVal toNumber As Integer)
        Me.ToNumber = toNumber
        Me.FromNumber = fromNumber
    End Sub

End Class
