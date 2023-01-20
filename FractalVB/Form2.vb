
Public Class frmMandelbrot
    Dim bmpMandel As New Bitmap(640, 640)
    Dim unitsPerPixel As Double = 0.0
    Dim minA As Double = -2.0
    Dim maxA As Double = 2.0
    Dim minB As Double = -2.0
    Dim maxB As Double = 2.0
    'maxA - minA = maxB - minB
    Dim IsDrawing As Boolean = False
    Dim IsRubberBand As Boolean = False
    Dim SelectedArea As Rubberband
    '  Dim colours As ColourSelector '---------------------------------------------

    Dim maxIterations As Integer = 500
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        '  colours = New ColourSelector '---------------------------------------------------



        SelectedArea = New Rubberband(picMandel)
        CreateMandelbrotImage()
    End Sub

    Public Function IsInMandelbrotSet(ByVal a As Double, ByVal b As Double, ByVal maxit As Integer) As Integer
        Dim squareOfDistance As Double
        Dim aSquare As Double = 0.0
        Dim bSquare As Double = 0.0
        Dim startA As Double = a
        Dim startB As Double = b
        Dim numIterations As Integer = 0
        While numIterations < maxit
            aSquare = a * a
            bSquare = b * b
            squareOfDistance = aSquare + bSquare
            If squareOfDistance > 4 Then
                Return numIterations
            End If
            b = 2 * a * b + startB
            a = (aSquare - bSquare) + startA
            numIterations += 1

        End While
        Return numIterations
    End Function


    Public Sub CreateMandelbrotImage()
        Dim cols() As Color = {Color.Black, Color.Gray, Color.DarkGray, Color.LightGray} '00000000000000000000000000000000000000000


        IsDrawing = True
        bmpMandel = New Bitmap(640, 640)
        unitsPerPixel = (maxA - minA) / bmpMandel.Width
        Dim numIterations As Integer = 0
        Dim pixelC As New Color
        Dim grey As Integer = 0
        Dim g As Graphics = Graphics.FromImage(bmpMandel)
        g.Clear(Color.White)
        g.Dispose()
        Cursor = Cursors.WaitCursor
        ToolStripProgressBar1.Visible = True
        ToolStripProgressBar1.Maximum = bmpMandel.Height

        For y As Integer = 0 To bmpMandel.Height - 1
            For x As Integer = 0 To bmpMandel.Width - 1
                Dim a As Double = minA + (x * unitsPerPixel)
                Dim b As Double = maxB - (y * unitsPerPixel)
                numIterations = IsInMandelbrotSet(a, b, maxIterations)
                If numIterations = maxIterations Then
                    ' is in set - colour black
                    bmpMandel.SetPixel(x, y, Color.Black) '00000000000000000000000000  Main fractal picture, 00000000000
                Else
                    'not in set - colour greyscale
                    'grey = CInt(255 * (CDbl(maxIterations - numIterations) / CDbl(maxIterations)))
                    'pixelC = Color.FromArgb(255, grey, grey, grey)
                    'not in set - colour 
                   
                    pixelC = cols(numIterations Mod cols.GetLength(0))
                    bmpMandel.SetPixel(x, y, pixelC)
                    bmpMandel.SetPixel(x, y, pixelC)

                    '----------------
                    Dim percentIterations As Double
                    percentIterations = (CDbl(numIterations) / CDbl(maxIterations))


                    'Dim rand As Random
                    'rand = New rand(0, 2) ------------------------------
                    '  Dim justInCase As Boolean = False
                    Dim i As Integer = 255
                    '  Dim k As Integer = 255
                    'Dim m As Integer = 255

                    Dim j As Decimal = 0.1

                    'While j > 0

                    '    If percentIterations >= j Then
                    '        pixelC = Color.FromArgb(255, i, k, m)

                    '    End If
                    '    j = j - (0.1 / 255) -------------------------------------------e
                    'End While
                   


                   


                    Dim truth As Boolean = False
                    'While truth = False
                    While i > 2 And truth = False
                        If percentIterations >= j Then
                            pixelC = Color.FromArgb(255, i, i, i)
                            truth = True
                            '  justInCase = True
                        End If
                        j = j - 0.00001307189542
                        i = i - 1
                    End While

                    '    i = 255
                    '    While k > 2 And truth = False

                    '        If percentIterations >= j Then
                    '            pixelC = Color.FromArgb(255, i, k, m)
                    '            truth = True
                    '            justInCase = True
                    '        End If
                    '        j = j - 0.00001307189542
                    '        k = k - 1
                    '    End While
                    '    k = 255

                    '    While m > 2 And truth = False

                    '        If percentIterations >= j Then
                    '            pixelC = Color.FromArgb(255, i, k, m)
                    '            truth = True
                    '            justInCase = True
                    '        End If
                    '        j = j - 0.00001307189542
                    '        m = m - 1
                    '    End While
                    '    m = 255
                    '    If justInCase = False Then
                    '        pixelC = Color.White
                    '    End If
                    'End While





                    bmpMandel.SetPixel(x, y, pixelC)


                    '---------------

                End If
            Next
            ToolStripProgressBar1.Value = y
            If (y Mod 20) = 0 Then
                picMandel.Image = bmpMandel
                Application.DoEvents()
            End If
        Next
        picMandel.Image = bmpMandel
        ToolStripProgressBar1.Visible = False
        Cursor = Cursors.[Default]
        IsDrawing = False


    End Sub



    '--------------------------------------------------------------------------
    Private Sub PLOTToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PLOTToolStripMenuItem.Click
        CreateMandelbrotImage()
    End Sub
  




    Private Sub picMandel_MouseMove(sender As Object, e As MouseEventArgs) Handles picMandel.MouseMove
        If Not IsDrawing Then
            Dim a As String = System.Convert.ToString((e.X * unitsPerPixel) + minA)
            Dim b As String = System.Convert.ToString(((bmpMandel.Height - e.Y) * unitsPerPixel) + minB)
            Dim coords As String = "(" + a + ", " + b + ")"
            ToolStripStatusLabel1.Text = coords
            If IsRubberBand Then
                SelectedArea.Move(e.X, e.Y)
            End If
        End If
    End Sub



    Private Sub picMandel_MouseDown(sender As Object, e As MouseEventArgs) Handles picMandel.MouseDown
        If Not IsDrawing Then
            SelectedArea.Start(e.X, e.Y)
            IsRubberBand = True
        End If
    End Sub


    Private Sub picMandel_MouseUp(sender As Object, e As MouseEventArgs) Handles picMandel.MouseUp
        If IsRubberBand Then
            If Not IsDrawing Then
                SelectedArea.[Stop]()
                IsRubberBand = False
                CreateNewView()
                CreateMandelbrotImage()
            End If
        End If
    End Sub
    Private Sub CreateNewView()
        Dim newRect As New Rectangle()
        Dim centrePoint As New Point(SelectedArea.SelectedRectangle.X + (SelectedArea.SelectedRectangle.Width / 2), SelectedArea.SelectedRectangle.Y + (SelectedArea.SelectedRectangle.Height / 2))
        'make into square the length of longest side
        If SelectedArea.SelectedRectangle.Width > SelectedArea.SelectedRectangle.Height Then
            newRect.Width = SelectedArea.SelectedRectangle.Width
            newRect.Height = newRect.Width
        Else
            newRect.Height = SelectedArea.SelectedRectangle.Width
            newRect.Width = newRect.Height
        End If
        'shift so that the square is centred on the centre of the rectangle
        Dim newOrigin As New Point()
        newOrigin.X = centrePoint.X - (newRect.Width / 2)
        newOrigin.Y = centrePoint.Y - (newRect.Height / 2)
        newRect.Location = newOrigin
        'convert into plot values
        minA = minA + (newRect.X * unitsPerPixel)
        maxA = minA + (newRect.Width * unitsPerPixel)
        maxB = maxB - (newRect.Y * unitsPerPixel)
        minB = maxB - (newRect.Height * unitsPerPixel)
    End Sub

    Private Sub SAVEToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SAVEToolStripMenuItem.Click
        If Not IsDrawing Then
            Dim sd As SaveFileDialog = New SaveFileDialog()
            sd.Title = "Save The Picture"
            sd.Filter = "Bitmap Image|*.bmp"

            If sd.ShowDialog = Windows.Forms.DialogResult.OK Then
                bmpMandel.Save(sd.FileName)


            End If
        End If
    End Sub

    Private Sub MenuStrip2_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip2.ItemClicked

    End Sub

    Private Sub picMandel_Click(sender As Object, e As EventArgs) Handles picMandel.Click

    End Sub
End Class
