Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class Message
    Private privateMessageText As String
    Public Property MessageText() As String
        Get
            Return privateMessageText
        End Get
        Set(ByVal value As String)
            privateMessageText = value
        End Set
    End Property
    Private privateSender As String
    Public Property Sender() As String
        Get
            Return privateSender
        End Get
        Set(ByVal value As String)
            privateSender = value
        End Set
    End Property
    Private privateSendTime As DateTime
    Public Property SendTime() As DateTime
        Get
            Return privateSendTime
        End Get
        Set(ByVal value As DateTime)
            privateSendTime = value
        End Set
    End Property

    Public Sub New(ByVal messageText As String, ByVal sender As String)
        Me.MessageText = messageText
        Me.Sender = sender
        SendTime = DateTime.Now
    End Sub

    Public Sub New()
    End Sub
End Class
