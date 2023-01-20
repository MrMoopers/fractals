using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalsCS
{
    public partial class MainForm : Form
    {
        public static List<Color> _colorPalette = null; //------------------------------I changed this to a static and public

        private Bitmap _bitmap;
        private bool _isDrawing = false;
        private RubberBand _selectedArea;
        private bool _isRubberBand = false;

        //private List<Color> _colorPalette = null;
        private int _maxIterations = 500;

        private double minA = 0.0;
        private double maxA = 0.0;
        private double minB = 0.0;
        private double maxB = 0.0;
        private int bitmapWidth = 640;
        private int bitmapHeight = 640;

        public MainForm()
        {
            InitializeComponent();

            _selectedArea = new RubberBand(picMandelbrot);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _bitmap = new Bitmap(bitmapWidth, bitmapHeight);
        }

        private List<Color> GetColorPalette()
        {
            minA = -2.0;
            maxA = 2.0;
            minB = -2.0;
            maxB = 2.0;

            ColorPaletteDialog colorPaletteDialog = new ColorPaletteDialog();
            DialogResult dialogResult = colorPaletteDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                return colorPaletteDialog.ColorPalette;
            }
            return null;
        }

        private void picMandelbrot_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_isDrawing)
            {
                _selectedArea.Start(e.X, e.Y);
                _isRubberBand = true;
            }
        }

        private void picMandelbrot_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing)
            {

                if (_isRubberBand)
                {
                    _selectedArea.Move(e.X, e.Y);
                }
            }
        }

        private void picMandelbrot_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isRubberBand)
            {
                if (!_isDrawing)
                {
                    _selectedArea.Stop();
                    _isRubberBand = false;

                    //Make a square the length of the longest selected side
                    Rectangle newRectangle = new Rectangle();
                    if (_selectedArea.SelectedRectangle.Width > _selectedArea.SelectedRectangle.Height)
                    {
                        newRectangle.Width = _selectedArea.SelectedRectangle.Width;
                        newRectangle.Height = _selectedArea.SelectedRectangle.Width;
                    }
                    else
                    {
                        newRectangle.Height = _selectedArea.SelectedRectangle.Height;
                        newRectangle.Width = _selectedArea.SelectedRectangle.Height;
                    }

                    //CreateNewView();
                    Point selectedRectangleCentrePoint = new Point();
                    selectedRectangleCentrePoint.X = _selectedArea.SelectedRectangle.X + (_selectedArea.SelectedRectangle.Width / 2);
                    selectedRectangleCentrePoint.Y = _selectedArea.SelectedRectangle.Y + (_selectedArea.SelectedRectangle.Height / 2);

                    //Shift so that the square is centred on the centre of the rectangle
                    Point newOrigin = new Point();
                    newOrigin.X = selectedRectangleCentrePoint.X - (newRectangle.Width / 2);
                    newOrigin.Y = selectedRectangleCentrePoint.Y - (newRectangle.Height / 2);
                    newRectangle.Location = newOrigin;

                    //Convert into plot values
                    double unitsPerPixel = (maxA - minA) / bitmapWidth;
                    minA = minA + (newRectangle.X * unitsPerPixel);
                    maxA = minA + (newRectangle.Width * unitsPerPixel);
                    minB = maxB - (newRectangle.Height * unitsPerPixel);
                    maxB = maxB - (newRectangle.Y * unitsPerPixel);

                    _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
                    picMandelbrot.Image = _bitmap;

                }
            }
        }



        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minA = -2.0;
            maxA = 2.0;
            minB = -2.0;
            maxB = 2.0;
            _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
            picMandelbrot.Image = _bitmap;
        }

        private void saveAsOriginalToolStripMenuItem_Click(object sender, EventArgs e)//Saves colour scheme at original 4X4 scale
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
            saveFileDialog.FileName = "ColorPaletteFile(ORIG)(0)";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");

            int i = 0;
            bool Exists = true;
            while (Exists == true)
            {
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("SaveAsStart", string.Format("ColorPaletteFile(ORIG)({0})", i)))) == true)
                {
                    i++;
                    saveFileDialog.FileName = string.Format("ColorPaletteFile(ORIG)({0})", i);
                }
                else
                {
                    Exists = false;
                }
            }

            saveFileDialog.ShowDialog();
            using (StreamWriter file = new StreamWriter(saveFileDialog.FileName))
            {
                foreach (Color c in _colorPalette)
                {
                    string line = String.Format("{0},{1},{2},{3}", c.A, c.R, c.G, c.B);
                    file.WriteLine(line);
                }
                file.Write(string.Format("!-2.0,2.0,-2.0,2.0"));
            }
        }

        private void saveCurrentAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
            saveFileDialog.FileName = "ColorPaletteFile(ZOOMED)(0)";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");

            int i = 0;
            bool Exists = true;
            while (Exists == true)
            {
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("SaveAsStart", string.Format("ColorPaletteFile(ZOOMED)({0})", i)))) == true)
                {
                    i++;
                    saveFileDialog.FileName = string.Format("ColorPaletteFile(ZOOMED)({0})", i);
                }
                else
                {
                    Exists = false;
                }
            }

            saveFileDialog.ShowDialog();
            using (StreamWriter file = new StreamWriter(saveFileDialog.FileName))
            {
                foreach (Color c in _colorPalette)
                {
                    string line = String.Format("{0},{1},{2},{3}", c.A, c.R, c.G, c.B);
                    file.WriteLine(line);
                }
                file.Write(string.Format("!{0},{1},{2},{3}", minA, maxA, minB, maxB));
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)//Opens colour scheme at current place (zoomed)
        {
            OpenFileDialog openTxt = new OpenFileDialog();
            openTxt.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");



            if (openTxt.ShowDialog() == DialogResult.OK)
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart", openTxt.FileName);
                _colorPalette = ColorPaletteFactory.GetColorPaletteFromFile(filePath);

                _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);

                picMandelbrot.Image = _bitmap;
            }
        }

        private void openAtOriginalPlaceToolStripMenuItem_Click(object sender, EventArgs e)//Opens colour scheme at original 4X4 scale
        {
            OpenFileDialog openTxt = new OpenFileDialog();
            openTxt.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");

            if (openTxt.ShowDialog() == DialogResult.OK)
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart", openTxt.FileName);
                _colorPalette = ColorPaletteFactory.GetColorPaletteFromFile(filePath);
                _bitmap = Mandelbrot.CreateImage(-2.0, 2.0, -2.0, 2.0, _maxIterations, _colorPalette, _bitmap);
                picMandelbrot.Image = _bitmap;
            }
        }

        private void openAtFilesPlaceToolStripMenuItem_Click(object sender, EventArgs e)//Opens colour scheme at the file's placement
        {
            OpenFileDialog openTxt = new OpenFileDialog();
            openTxt.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");



            if (openTxt.ShowDialog() == DialogResult.OK)
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart", openTxt.FileName);

                _colorPalette = ColorPaletteFactory.GetColorPaletteFromFile(filePath);
                List<Double> _coords = ColorPaletteFactory.GetCoordinates(filePath);

                minA = _coords[0];
                maxA = _coords[1];
                minB = _coords[2];
                maxB = _coords[3];
                _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
                picMandelbrot.Image = _bitmap;
            }
        }

        private void convertCurrentToBitmapToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string blankFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fractal_Blank.png");


            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitmaps", "Fractal_Blank.png")))
            {
                File.Copy(blankFilePath, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitmaps", "Fractal_Blank.png"));
            }

            _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
            saveFileDialog.Filter = "PNG Files (*.png)|*.png|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            SaveFileDialog savefiledialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitmaps");
            saveFileDialog.FileName = "NewFractal";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            string saveFilePath = saveFileDialog.FileName;

            string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitmaps", saveFilePath + ".png");

            _bitmap.Save(test);



        }



        private void newFractalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            _colorPalette = GetColorPalette();
            if (_colorPalette != null)
            {
                minA = -2.0;
                maxA = 2.0;
                minB = -2.0;
                maxB = 2.0;
                _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
            }
            picMandelbrot.Image = _bitmap;
        }

        private void restartThisColourPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _bitmap = new Bitmap(bitmapWidth, bitmapHeight);




            minA = -2.0;
            maxA = 2.0;
            minB = -2.0;
            maxB = 2.0;
            _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);

            picMandelbrot.Image = _bitmap;
        }

        #region Depricated Code

        //Public Sub CreateMandelbrotImage()
        //    Dim cols() As Color = {Color.Black, Color.Gray, Color.DarkGray, Color.LightGray} '00000000000000000000000000000000000000000


        //    IsDrawing = True
        //    bmpMandel = New Bitmap(640, 640)
        //    unitsPerPixel = (maxA - minA) / bmpMandel.Width
        //    Dim numIterations As Integer = 0
        //    Dim pixelC As New Color
        //    Dim grey As Integer = 0
        //    Dim g As Graphics = Graphics.FromImage(bmpMandel)
        //    g.Clear(Color.White)
        //    g.Dispose()
        //    Cursor = Cursors.WaitCursor
        //    ToolStripProgressBar1.Visible = True
        //    ToolStripProgressBar1.Maximum = bmpMandel.Height

        //    For y As Integer = 0 To bmpMandel.Height - 1
        //        For x As Integer = 0 To bmpMandel.Width - 1
        //            Dim a As Double = minA + (x * unitsPerPixel)
        //            Dim b As Double = maxB - (y * unitsPerPixel)
        //            numIterations = IsInMandelbrotSet(a, b, maxIterations)
        //            If numIterations = maxIterations Then
        //                ' is in set - colour black
        //                bmpMandel.SetPixel(x, y, Color.Black) '00000000000000000000000000  Main fractal picture, 00000000000
        //            Else
        //                'not in set - colour greyscale
        //                'grey = CInt(255 * (CDbl(maxIterations - numIterations) / CDbl(maxIterations)))
        //                'pixelC = Color.FromArgb(255, grey, grey, grey)
        //                'not in set - colour 

        //                pixelC = cols(numIterations Mod cols.GetLength(0))
        //                bmpMandel.SetPixel(x, y, pixelC)
        //                bmpMandel.SetPixel(x, y, pixelC)

        //                '----------------
        //                Dim percentIterations As Double
        //                percentIterations = (CDbl(numIterations) / CDbl(maxIterations))


        //                'Dim rand As Random
        //                'rand = New rand(0, 2) ------------------------------
        //                '  Dim justInCase As Boolean = False
        //                Dim i As Integer = 255
        //                '  Dim k As Integer = 255
        //                'Dim m As Integer = 255

        //                Dim j As Decimal = 0.1

        //                'While j > 0

        //                '    If percentIterations >= j Then
        //                '        pixelC = Color.FromArgb(255, i, k, m)

        //                '    End If
        //                '    j = j - (0.1 / 255) -------------------------------------------e
        //                'End While






        //                Dim truth As Boolean = False
        //                'While truth = False
        //                While i > 2 And truth = False
        //                    If percentIterations >= j Then
        //                        pixelC = Color.FromArgb(255, i, i, i)
        //                        truth = True
        //                        '  justInCase = True
        //                    End If
        //                    j = j - 0.00001307189542
        //                    i = i - 1
        //                End While

        //                '    i = 255
        //                '    While k > 2 And truth = False

        //                '        If percentIterations >= j Then
        //                '            pixelC = Color.FromArgb(255, i, k, m)
        //                '            truth = True
        //                '            justInCase = True
        //                '        End If
        //                '        j = j - 0.00001307189542
        //                '        k = k - 1
        //                '    End While
        //                '    k = 255

        //                '    While m > 2 And truth = False

        //                '        If percentIterations >= j Then
        //                '            pixelC = Color.FromArgb(255, i, k, m)
        //                '            truth = True
        //                '            justInCase = True
        //                '        End If
        //                '        j = j - 0.00001307189542
        //                '        m = m - 1
        //                '    End While
        //                '    m = 255
        //                '    If justInCase = False Then
        //                '        pixelC = Color.White
        //                '    End If
        //                'End While





        //                bmpMandel.SetPixel(x, y, pixelC)


        //                '---------------

        //            End If
        //        Next
        //        ToolStripProgressBar1.Value = y
        //        If (y Mod 20) = 0 Then
        //            picMandel.Image = bmpMandel
        //            Application.DoEvents()
        //        End If
        //    Next
        //    picMandel.Image = bmpMandel
        //    ToolStripProgressBar1.Visible = False
        //    Cursor = Cursors.[Default]
        //    IsDrawing = False


        //End Sub

        #endregion

        private void MainForm_Shown(object sender, EventArgs e)
        {
            _colorPalette = GetColorPalette();
            if (_colorPalette != null)
            {
                _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
                picMandelbrot.Image = _bitmap;
            }
        }
    }
}
