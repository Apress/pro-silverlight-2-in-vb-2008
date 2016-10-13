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

Public MustInherit Class PageTransitionBase
    ' For better encapsulation these could be private fields wrapped by protected properties.
    Protected storyboard As New Storyboard()
    Protected oldPage As UserControl
    Protected newPage As UserControl

    Public Sub New()
        AddHandler storyboard.Completed, AddressOf TransitionCompleted
    End Sub

    Public Sub Navigate(ByVal newPage As UserControl)
        ' Set the pages.
        Me.newPage = newPage
        Dim grid As Grid = CType(Application.Current.RootVisual, Grid)
        oldPage = CType(grid.Children(0), UserControl)

        ' Insert the new page first (so it lies "behind" the old page).
        grid.Children.Insert(0, newPage)

        ' Prepared the animation.
        PrepareStoryboard()

        ' Perform the animation.            
        storyboard.Begin()
    End Sub

    Private Sub TransitionCompleted(ByVal sender As Object, ByVal e As EventArgs)
        ' This is the place to perform clean up.
        ' In this example the animation acts on the old page,
        ' which is discarded after the navigation. Thus, there's no
        ' need to explicitly remove the storyboard from the Resources
        ' collection of the page.

        ' Remove the old page, which is not needed any longer.
        Dim grid As Grid = CType(Application.Current.RootVisual, Grid)
        grid.Children.Remove(oldPage)
    End Sub

    ' Derived classes override this method to create and configure the animations.
    Protected MustOverride Sub PrepareStoryboard()
End Class



Public Class WipeTransition
    Inherits PageTransitionBase
    Protected Overrides Sub PrepareStoryboard()
        ' Create the opacity mask.
        Dim mask As New LinearGradientBrush()
        mask.StartPoint = New Point(0,0)
        mask.EndPoint = New Point(1,0)

        Dim transparentStop As New GradientStop()
        transparentStop.Color = Colors.Transparent
        transparentStop.Offset = 0
        mask.GradientStops.Add(transparentStop)
        Dim visibleStop As New GradientStop()
        visibleStop.Color = Colors.Black
        visibleStop.Offset = 0
        mask.GradientStops.Add(visibleStop)

        oldPage.OpacityMask = mask

        ' Create the animations for the opacity mask.          
        Dim visibleStopAnimation As New DoubleAnimation()
        Storyboard.SetTarget(visibleStopAnimation, visibleStop)
        Storyboard.SetTargetProperty(visibleStopAnimation, New PropertyPath("Offset"))
        visibleStopAnimation.Duration = TimeSpan.FromSeconds(1.2)
        visibleStopAnimation.From = 0
        visibleStopAnimation.To = 1.2

        Dim transparentStopAnimation As New DoubleAnimation()
        Storyboard.SetTarget(transparentStopAnimation, transparentStop)
        Storyboard.SetTargetProperty(transparentStopAnimation, New PropertyPath("Offset"))
        transparentStopAnimation.BeginTime = TimeSpan.FromSeconds(0.2)
        transparentStopAnimation.From = 0
        transparentStopAnimation.To = 1
        transparentStopAnimation.Duration = TimeSpan.FromSeconds(1)

        ' Add the animations to the storyboard.
        storyboard.Children.Add(transparentStopAnimation)
        storyboard.Children.Add(visibleStopAnimation)
    End Sub
End Class
