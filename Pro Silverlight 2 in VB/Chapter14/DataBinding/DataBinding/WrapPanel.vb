Public Class WrapPanel
    Inherits Panel

    Protected Overrides Function MeasureOverride(ByVal constraint As Size) As Size
        Dim currentLineSize As New Size()
        Dim panelSize As New Size()

        ' Examine all the elements in this panel.
        For Each element As UIElement In Me.Children
            ' Get the desired size of the element.
            element.Measure(constraint)
            Dim desiredSize As Size = element.DesiredSize

            ' Check if the element fits in the line, if given its desired size.
            If currentLineSize.Width + desiredSize.Width > constraint.Width Then
                ' Switch to a new line because space has run out.
                panelSize.Height += currentLineSize.Height
                panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width)
                currentLineSize = desiredSize

                ' If the element is too wide to fit using the maximum width of the line,
                ' just give it a separate line.
                If desiredSize.Width > constraint.Width Then
                    ' Make the width of the element the new desired width.
                    panelSize.Width = Math.Max(desiredSize.Width, panelSize.Width)
                End If
            Else
                ' Add the element to the current line.
                currentLineSize.Width += desiredSize.Width

                ' Make sure the line is as tall as its tallest element.
                currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height)
            End If
        Next

        ' Return the size required to fit all elements.
        ' Ordinarily, this is the width of the constraint, and the height
        ' required to fit all the elements.
        ' However, if an element is wider than the width given to the panel,
        ' the desired width will be the width of that line.
        panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width)
        panelSize.Height += currentLineSize.Height
        Return panelSize
    End Function

    Protected Overrides Function ArrangeOverride(ByVal arrangeBounds As Size) As Size
        Dim currentLineSize As New Size()
        Dim totalHeight As Double = 0

        ' Examine all the elements in this panel.
        For Each element As UIElement In Me.Children
            ' Get the desired size of the element, but don't call Measure() again,
            ' or that will trigger the measure layout pass and MeasureOverride()!               
            Dim desiredSize As Size = element.DesiredSize

            ' Check if the element fits in the line, if given its desired size.
            If currentLineSize.Width + desiredSize.Width > arrangeBounds.Width Then
                ' Switch to a new line because space has run out.
                totalHeight += currentLineSize.Height
                currentLineSize = New Size()
            End If

            ' Make sure the line is as tall as its tallest element.
            currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height)

            ' Place the element on the line, giving it its desired size.
            element.Arrange(New Rect(currentLineSize.Width, totalHeight, element.DesiredSize.Width, element.DesiredSize.Height))

            ' Move over for the next element.
            currentLineSize.Width += desiredSize.Width
        Next

        ' Return the size this panel actually occupies.
        totalHeight += currentLineSize.Height
        Return New Size(arrangeBounds.Width, totalHeight)
    End Function


    Private Sub arrangeLine(ByVal y As Double, ByVal lineHeight As Double, ByVal start As Integer, ByVal [end] As Integer)
        Dim x As Double = 0
        Dim children As UIElementCollection = Me.Children
        For i As Integer = start To [end] - 1
            Dim child As UIElement = children(i)
            child.Arrange(New Rect(x, y, child.DesiredSize.Width, lineHeight))
            x += child.DesiredSize.Width
        Next
    End Sub

End Class

