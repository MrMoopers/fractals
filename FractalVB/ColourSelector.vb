Public Class ColourSelector


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If ColorDialog1.ShowDialog() Then
            TextBox1.BackColor() = ColorDialog1.Color()


        End If


    End Sub
End Class