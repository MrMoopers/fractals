
Class Rubberband

    Public Enum RubberBandState
        Inactive
        Starting
        Moving
    End Enum

    Private StartPoint As Point
    Private EndPoint As Point
    Private CurrentState As RubberBandState = RubberBandState.Inactive
    Private Surface As Control

    Public Sub New(ByVal ControlSurface As Control)
        Surface = ControlSurface
    End Sub

    Public ReadOnly Property SelectedRectangle() As Rectangle
        Get
            Dim selectedRect As New Rectangle()
            selectedRect.X = If(StartPoint.X < EndPoint.X, StartPoint.X, EndPoint.X)
            selectedRect.Y = If(StartPoint.Y < EndPoint.Y, StartPoint.Y, EndPoint.Y)
            selectedRect.Width = Math.Abs(EndPoint.X - StartPoint.X)
            selectedRect.Height = Math.Abs(EndPoint.Y - StartPoint.Y)
            Return selectedRect
        End Get
    End Property

    Public Sub Start(ByVal x As Integer, ByVal y As Integer)
        StartPoint.X = x
        StartPoint.Y = y
        EndPoint.X = x
        EndPoint.Y = y
        KeepInView(StartPoint)
        CurrentState = RubberBandState.Starting
    End Sub

    Public Sub [Stop]()
        DrawFrame()
        CurrentState = RubberBandState.Inactive
    End Sub

    Private Sub KeepInView(ByRef origin As Point)
        If origin.X < 0 Then
            origin.X = 0
        End If
        If origin.X > Surface.ClientSize.Width Then
            origin.X = Surface.ClientSize.Width - 1
        End If
        If origin.Y < 0 Then
            origin.Y = 0
        End If
        If origin.Y > Surface.ClientSize.Height Then
            origin.Y = Surface.ClientSize.Height - 1
        End If
    End Sub

    Public Sub Move(ByVal x As Integer, ByVal y As Integer)
        Dim newPoint As New Point(x, y)
        KeepInView(newPoint)
        Select Case CurrentState
            Case RubberBandState.Inactive
                Exit Select
            Case RubberBandState.Starting
                EndPoint = newPoint
                DrawFrame()
                CurrentState = RubberBandState.Moving
                Exit Select
            Case RubberBandState.Moving
                DrawFrame()
                EndPoint = newPoint
                DrawFrame()
                Exit Select
        End Select
    End Sub

    Private Sub DrawFrame()
        Dim exactStart As Point = Surface.PointToScreen(StartPoint)
        Dim exactEnd As Point = Surface.PointToScreen(EndPoint)
        Dim rectSize As New Size(exactEnd.X - exactStart.X, exactEnd.Y - exactStart.Y)
        Dim drawRect As New Rectangle(Surface.PointToScreen(StartPoint), rectSize)
        ControlPaint.DrawReversibleFrame(drawRect, Color.Black, FrameStyle.Dashed)
    End Sub

End Class
