Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.Windows.Controls.Primitives

<TemplatePart(Name := "Content", Type := GetType(FrameworkElement)), TemplatePart(Name := "ExpandCollapseButton", Type := GetType(ToggleButton)), TemplateVisualState(Name := "Expanded", GroupName := "ViewStates"), TemplateVisualState(Name := "Collapsed", GroupName:="ViewStates")> _
Public Class Expander
    Inherits ContentControl
    Public Sub New()
        DefaultStyleKey = GetType(Expander)
    End Sub


    Private Sub ChangeVisualState(ByVal useTransitions As Boolean)
        '  Apply the current state from the ViewStates group.
        If IsExpanded Then
            If Not contentElement Is Nothing Then
            contentElement.Visibility = Visibility.Visible
            End If

           VisualStateManager.GoToState(Me, "Expanded", useTransitions)
        Else
            VisualStateManager.GoToState(Me, "Collapsed", useTransitions)
            If collapsedState Is Nothing Then
                ' There is no state animation, so just hide the content region immediately.
                If contentElement IsNot Nothing Then
                    contentElement.Visibility = Visibility.Collapsed
                End If
            End If
        End If

        ' (If there were other state groups, you would set them now.)           
    End Sub

    Public Shared ReadOnly CornerRadiusProperty As DependencyProperty = DependencyProperty.Register("CornerRadius", GetType(CornerRadius), GetType(Expander), Nothing)
    Public Property CornerRadius() As CornerRadius
        Get
            Return CType(GetValue(CornerRadiusProperty), CornerRadius)
        End Get
        Set(ByVal value As CornerRadius)
            SetValue(CornerRadiusProperty, value)
        End Set
    End Property

    Public Shared ReadOnly HeaderContentProperty As DependencyProperty = DependencyProperty.Register("HeaderContent", GetType(Object), GetType(Expander), Nothing)
    Public Property HeaderContent() As Object
        Get
            Return CObj(GetValue(HeaderContentProperty))
        End Get
        Set(ByVal value As Object)
            SetValue(HeaderContentProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IsExpandedProperty As DependencyProperty = DependencyProperty.Register("IsExpanded", GetType(Boolean), GetType(Expander), New PropertyMetadata(True))

    Private cmdExpandOrCollapse As ToggleButton
    Private contentElement As FrameworkElement
    Private collapsedState As VisualState

    Public Overrides Sub OnApplyTemplate()
        MyBase.OnApplyTemplate()

        contentElement = TryCast(GetTemplateChild("Content"), FrameworkElement)
        If contentElement IsNot Nothing Then
            collapsedState = TryCast(GetTemplateChild("Collapsed"), VisualState)
            If (collapsedState IsNot Nothing) AndAlso (collapsedState.Storyboard IsNot Nothing) Then
                AddHandler collapsedState.Storyboard.Completed, AddressOf collapsedStoryboard_Completed
            End If
        End If

        cmdExpandOrCollapse = TryCast(GetTemplateChild("ExpandCollapseButton"), ToggleButton)
        If cmdExpandOrCollapse IsNot Nothing Then
            AddHandler cmdExpandOrCollapse.Click, AddressOf cmdExpandCollapseButton_Click
        End If
        ChangeVisualState(False)
    End Sub

    Private Sub collapsedStoryboard_Completed(ByVal sender As Object, ByVal e As EventArgs)
        contentElement.Visibility = Visibility.Collapsed
    End Sub

    Public Property IsExpanded() As Boolean
        Get
            Return CBool(GetValue(IsExpandedProperty))
        End Get
        Set(ByVal value As Boolean)
            SetValue(IsExpandedProperty, value)
            If cmdExpandOrCollapse IsNot Nothing Then
                cmdExpandOrCollapse.IsChecked = IsExpanded
            End If
        End Set
    End Property


    Private Sub cmdExpandCollapseButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ExpandOrCollapse(True)
    End Sub

    Public Sub ExpandOrCollapse(ByVal useTransitions As Boolean)
        IsExpanded = Not IsExpanded

        ChangeVisualState(useTransitions)
    End Sub

End Class
