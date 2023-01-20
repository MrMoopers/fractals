using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FractalFactory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Color[] colors = { Colors.Red, Colors.Blue, Colors.Green, Colors.Yellow };


        //private static List<Color> _colorPalette = GetColorPalette1();
        private static List<Color> _colorPalette = GetColorPalette2(colors);
        //private static List<Color> _colorPalette = GetColorPalette3();

        private static Storage storage = new Storage();
        private StartUpWindow startUpWindow;

        private Point startPoint;
        private Rectangle newRectangle = null;
        private int _maxIterations = 50;

        private FractalFactoryViewState<decimal> viewStateDecimal = null;
        private FractalFactoryViewState<double> viewStateDouble = null;
        private List<FractalFactoryViewState<decimal>> viewStateListDecimal = null;
        private List<FractalFactoryViewState<double>> viewStateListDouble = null;
        private int viewStateListCounter = 0;
        private Mandelbrot<decimal> mandelbrotDecimal = null;
        private Mandelbrot<double> mandelbrotDouble = null;

        private Byte[] fractalByteArray;

        private List<Byte> fractalByteArray_AllWhite = new List<byte>();




        public MainWindow()
        {
            startUpWindow = new StartUpWindow();

            InitializeComponent();

            //Creating a new dataGrid
            List<NavigationItem> navigationItems = new List<NavigationItem>();
            //NavigationItem navItem = new NavigationItem();
            //nI.Position = 0;

            //navigationItems.Add(nI);
            //DataGrid.ItemsSource = navigationItems;



            fractalByteArray = new Byte[1638400];

            MessageBoxResult dialogResult = MessageBox.Show("Decimal?", "Use Decimal?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

            if (dialogResult == MessageBoxResult.Yes)
            {
                storage.Choice = "Decimal";

                viewStateDecimal = new FractalFactoryViewState<decimal>();
                viewStateListDecimal = new List<FractalFactoryViewState<decimal>>();
                viewStateListCounter = 0;

                mandelbrotDecimal = new Mandelbrot<decimal>();

                viewStateDecimal.CreateNextState(2.0m, 2.0m, -2.0m, -2.0m, _maxIterations, _colorPalette, Convert.ToInt32(fractalImage.Width), Convert.ToInt32(fractalImage.Height) / 4);
                viewStateListDecimal.Add(viewStateDecimal);

                AwaitCreateImage(viewStateDecimal);
            }
            else
            {
                storage.Choice = "Double";

                viewStateDouble = new FractalFactoryViewState<double>();
                viewStateListDouble = new List<FractalFactoryViewState<double>>();
                viewStateListCounter = 0;

                mandelbrotDouble = new Mandelbrot<double>();

                viewStateDouble.CreateNextState(2.0, 2.0, -2.0, -2.0, _maxIterations, _colorPalette, Convert.ToInt32(fractalImage.Width), Convert.ToInt32(fractalImage.Height));
                viewStateListDouble.Add(viewStateDouble);

                AwaitCreateImage(viewStateDouble);
            }






        }
        public class NavigationItem
        {
            public int Position { get; set; }
            public BitmapSource Image { get; set; }
        }
        private async void AwaitCreateImage(FractalFactoryViewState<decimal> viewStateDecimal)
        {
            await CreateImageDecimal(viewStateDecimal);
        }

        private async void AwaitCreateImage(FractalFactoryViewState<double> viewStateDouble)
        {
            await CreateImageDouble(viewStateDouble);
        }

        private async Task CreateImageDecimal(FractalFactoryViewState<decimal> viewStateDecimal)
        {

            mandelbrotDecimal.RaisePixelCreatedEvent += HandlePixelCreatedEvent;
            mandelbrotDecimal.RaisePixelCollectionCreatedEvent += HandlePixelCollectionCreatedEvent;

            Task[] taskList = new Task[]
            {
                Task.Run(() => mandelbrotDecimal.CreateImage(viewStateDecimal, new Point(0, 640 * 0 / 4), new Point(640, 640 * 1 / 4))),
                Task.Run(() => mandelbrotDecimal.CreateImage(viewStateDecimal, new Point(0, 640 * 1 / 4), new Point(640, 640 * 2 / 4))),
                Task.Run(() => mandelbrotDecimal.CreateImage(viewStateDecimal, new Point(0, 640 * 2 / 4), new Point(640, 640 * 3 / 4))),
                Task.Run(() => mandelbrotDecimal.CreateImage(viewStateDecimal, new Point(0, 640 * 3 / 4), new Point(640, 640 * 4 / 4))),
            };
            await Task.WhenAll(taskList);
            #region previousloop
            //                viewStateDouble = new FractalFactoryViewState<double>();
            //                viewStateListDouble = new List<FractalFactoryViewState<double>>();
            //                viewStateListCounter = 0;

            //                mandelbrotDouble = new Mandelbrot<double>();

            //                viewStateDouble.CreateNextState(2.0, 2.0, -2.0, -2.0, _maxIterations, _colorPalette, Convert.ToInt32(fractalImage.Width), Convert.ToInt32(fractalImage.Height));
            //                viewStateListDouble.Add(viewStateDouble);
            //                await Task.Run(() =>
            //                {
            //        mandelbrotDouble.RaisePixelCreatedEvent += HandlePixelCreatedEvent;
            //        mandelbrotDouble.RaisePixelCollectionCreatedEvent += HandlePixelCollectionCreatedEvent;
            //        mandelbrotDouble.CreateImage(viewStateDouble);
            //    });



            //mandelbrotDecimal.RaisePixelCreatedEvent += HandlePixelCreatedEvent;
            //mandelbrotDecimal.RaisePixelCollectionCreatedEvent += HandlePixelCollectionCreatedEvent;

            //int numberOfTasks = 4;
            ////int rowsPerTask = (int)fractalCanvas.Height/ numberOfTasks;

            ////int rowCount = rowsPerTask;
            ////int colStart = 0;
            ////int colCount = (int)fractalCanvas.Width;

            //Point startPoint = new Point(0, -fractalCanvas.Height / 4);
            //Point endPoint = new Point(fractalCanvas.Width, 0);

            ////List<Task> taskList = new List<Task>();
            //for (int i = 0; i < numberOfTasks; i++)
            //{
            //    //int rowStart = (i * rowsPerTask);

            //    startPoint.Y += fractalCanvas.Height / 4;
            //    endPoint.Y += fractalCanvas.Height / 4;

            //    //taskList.Add(Task.Run(() =>
            //    //{
            //        mandelbrotDecimal.CreateImage(viewStateDecimal, startPoint, endPoint);
            //    //}));
            //}

            //await Task.WhenAll(taskList.ToArray());
            #endregion


            LoadBitMap();



            //RefreshBtnBackBtnForwardOpacity();
        }

        private async Task CreateImageDouble(FractalFactoryViewState<double> viewStateDouble)
        {

            mandelbrotDouble.RaisePixelCreatedEvent += HandlePixelCreatedEvent;
            mandelbrotDouble.RaisePixelCollectionCreatedEvent += HandlePixelCollectionCreatedEvent;

            Task[] taskList = new Task[]
            {
                Task.Run(() => mandelbrotDouble.CreateImage(viewStateDouble, new Point(0, 640 * 0 / 4), new Point(640, 640 * 1 / 4))),
                Task.Run(() => mandelbrotDouble.CreateImage(viewStateDouble, new Point(0, 640 * 1 / 4), new Point(640, 640 * 2 / 4))),
                Task.Run(() => mandelbrotDouble.CreateImage(viewStateDouble, new Point(0, 640 * 2 / 4), new Point(640, 640 * 3 / 4))),
                Task.Run(() => mandelbrotDouble.CreateImage(viewStateDouble, new Point(0, 640 * 3 / 4), new Point(640, 640 * 4 / 4))),
            };
            await Task.WhenAll(taskList);



            #region previousloop
            //mandelbrotDecimal.RaisePixelCreatedEvent += HandlePixelCreatedEvent;
            //mandelbrotDecimal.RaisePixelCollectionCreatedEvent += HandlePixelCollectionCreatedEvent;

            //int numberOfTasks = 4;
            ////int rowsPerTask = (int)fractalCanvas.Height/ numberOfTasks;

            ////int rowCount = rowsPerTask;
            ////int colStart = 0;
            ////int colCount = (int)fractalCanvas.Width;

            //Point startPoint = new Point(0, -fractalCanvas.Height / 4);
            //Point endPoint = new Point(fractalCanvas.Width, 0);

            ////List<Task> taskList = new List<Task>();
            //for (int i = 0; i < numberOfTasks; i++)
            //{
            //    //int rowStart = (i * rowsPerTask);

            //    startPoint.Y += fractalCanvas.Height / 4;
            //    endPoint.Y += fractalCanvas.Height / 4;

            //    //taskList.Add(Task.Run(() =>
            //    //{
            //        mandelbrotDecimal.CreateImage(viewStateDecimal, startPoint, endPoint);
            //    //}));
            //}

            //await Task.WhenAll(taskList.ToArray());
            #endregion


            LoadBitMap();



            //RefreshBtnBackBtnForwardOpacity();
        }


        #region PreviousCreateStartingImage...
        //                 //Byte[] fractalbyteArray = null;
        //            if (storage.Choice == "Decimal")
        //            {
        //                viewStateDecimal = new FractalFactoryViewState<decimal>();
        //                viewStateListDecimal = new List<FractalFactoryViewState<decimal>>();
        //                viewStateListCounter = 0;

        //                mandelbrotDecimal = new Mandelbrot<decimal>();

        //                viewStateDecimal.CreateNextState(2.0m, 2.0m, -2.0m, -2.0m, _maxIterations, _colorPalette, Convert.ToInt32(fractalImage.Width), Convert.ToInt32(fractalImage.Height)/4);
        //                viewStateListDecimal.Add(viewStateDecimal);

        //                await Task.Run(() =>
        //                {
        //            mandelbrotDecimal.RaisePixelCreatedEvent += HandlePixelCreatedEvent;
        //            mandelbrotDecimal.RaisePixelCollectionCreatedEvent += HandlePixelCollectionCreatedEvent;
        //            mandelbrotDecimal.CreateImage(viewStateDecimal);
        //        });
        //            }
        //            else
        //            {
        //                viewStateDouble = new FractalFactoryViewState<double>();
        //                viewStateListDouble = new List<FractalFactoryViewState<double>>();
        //                viewStateListCounter = 0;

        //                mandelbrotDouble = new Mandelbrot<double>();

        //                viewStateDouble.CreateNextState(2.0, 2.0, -2.0, -2.0, _maxIterations, _colorPalette, Convert.ToInt32(fractalImage.Width), Convert.ToInt32(fractalImage.Height));
        //                viewStateListDouble.Add(viewStateDouble);
        //                await Task.Run(() =>
        //                {
        //        mandelbrotDouble.RaisePixelCreatedEvent += HandlePixelCreatedEvent;
        //        mandelbrotDouble.RaisePixelCollectionCreatedEvent += HandlePixelCollectionCreatedEvent;
        //        mandelbrotDouble.CreateImage(viewStateDouble);
        //    });

        //            }
        //LoadBitMap();

        //RefreshBtnBackBtnForwardOpacity();
        #endregion

        int counter = 0;
        private void HandlePixelCollectionCreatedEvent(object sender, PixelCollectionCreatedEventArgs e)
        {

            Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, (Action)delegate
            {
                int _offset = e.Offset;
                foreach (Color color in e.Colors)
                {
                    fractalByteArray[_offset + 0] = color.B;
                    fractalByteArray[_offset + 1] = color.G;
                    fractalByteArray[_offset + 2] = color.R;
                    fractalByteArray[_offset + 3] = color.A;
                    _offset += 4;
                }

                if ((bool)CheckBox_SegmentedDisplay.IsChecked)
                {
                    LoadBitMap();
                }


                //Thread.Sleep(1);
                //Thread.Yield();

            });

        }

        //List<PixelCreatedEventArgs> pixelCreatedEventArgs = new List<PixelCreatedEventArgs>();

        private void HandlePixelCreatedEvent(object sender, PixelCreatedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
            {
                fractalByteArray[e.Offset + 0] = e.Color.B;
                fractalByteArray[e.Offset + 1] = e.Color.G;
                fractalByteArray[e.Offset + 2] = e.Color.R;
                fractalByteArray[e.Offset + 3] = e.Color.A;


                if (counter % 640 == 0)
                {
                    LoadBitMap();
                    Thread.Sleep(1);
                }

                counter++;

            });
        }

        private void RefreshBtnBackBtnForwardOpacity()
        {
            if (viewStateListCounter == 0)
            {
                BtnBack.Opacity = 0.5;
            }
            else
            {
                BtnBack.Opacity = 1;
            }

            if (viewStateListCounter == (viewStateListDouble == null ? viewStateListDecimal.Count - 1 : viewStateListDouble.Count))
            {
                BtnForward.Opacity = 0.5;
            }
            else
            {
                BtnForward.Opacity = 1;
            }
        }

        private void LoadBitMap()
        {
            int width = Convert.ToInt32(fractalImage.Width);
            int height = Convert.ToInt32(fractalImage.Height);
            var dpiX = 96d;
            var dpiY = 96d;
            var pixelFormat = PixelFormats.Pbgra32;
            var bytesPerPixel = (pixelFormat.BitsPerPixel + 7) / 8;
            var stride = bytesPerPixel * width;

            int bufferSize = width * height * bytesPerPixel;



            var bitmap = BitmapSource.Create(width, height, dpiX, dpiY, pixelFormat, null, fractalByteArray, stride);
            WriteableBitmap writeableBitmap = new WriteableBitmap(ConvertToPbgra32Format(bitmap));

            //NavigationItem navItem = new NavigationItem();
            //navItem.Position = 

            MyBitmap = writeableBitmap;
        }

        public WriteableBitmap MyBitmap
        {
            get { return (WriteableBitmap)GetValue(MyBitmapProperty); }
            set { SetValue(MyBitmapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyBitmap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyBitmapProperty =
            DependencyProperty.Register("MyBitmap", typeof(WriteableBitmap), typeof(MainWindow), new PropertyMetadata(null));

        public static WriteableBitmap ConvertToPbgra32Format(BitmapSource source)
        {
            // Convert to Pbgra32 if it's a different format
            if (source.Format == PixelFormats.Pbgra32)
            {
                return new WriteableBitmap(source);
            }

            var formatedBitmapSource = new FormatConvertedBitmap();
            formatedBitmapSource.BeginInit();
            formatedBitmapSource.Source = source;
            formatedBitmapSource.DestinationFormat = PixelFormats.Pbgra32;
            formatedBitmapSource.EndInit();
            return new WriteableBitmap(formatedBitmapSource);
        }

        public static List<Color> GetColorPalette1()
        {
            List<Color> colorPalette = new List<Color>();
            for (int i = 0; i < 10; i++)
            {
                colorPalette.Add(Color.FromArgb(0, (byte)((i * 37) % 256), (byte)((i * 53) % 256), (byte)((i * 19) % 256)));
            }
            return colorPalette;
        }

        public static List<Color> GetColorPalette2(Color[] Colors)
        {
            List<Color> colorPalette = new List<Color>();
            for (int i = 0; i < Colors.Length; i++)
            {
                if (Colors[i] != null)
                {
                    colorPalette.Add((Colors[i]));

                }
            }
            return colorPalette;
        }
        public static List<Color> GetColorPalette3()
        {
            List<Color> colorPalette = new List<Color>();
            for (int i = 0; i < 255; i++)
            {
                //colorPalette.Add(Color.FromArgb(0, (byte)((i * 3) % 256), (byte)((i * 7) % 256), (byte)((i * 5) % 256)));
                //colorPalette.Add(Color.FromArgb(0, (byte)((i * 11) % 256), (byte)((i * 13) % 256), (byte)((i * 7) % 256))); // lower numbers = more gradient colours
                //colorPalette.Add(Color.FromArgb(0, (byte)((i * 11) % 256), (byte)((i * 57) % 256), (byte)((i * 47) % 256)));

                colorPalette.Add(Color.FromArgb(0, (byte)(i*2 % 256), 0, 0));
            }
            return colorPalette;
        }

        private void fractalCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            newRectangle = new Rectangle();
            newRectangle.Stroke = Brushes.White;
            newRectangle.Fill = Brushes.Transparent;
            newRectangle.StrokeDashArray = new DoubleCollection() { 2 };
            newRectangle.Width = 0;
            newRectangle.Height = 0;

            startPoint = e.GetPosition(fractalCanvas);//Doesn't seem to care if startpoint is actually not on canvas

            Canvas.SetTop(newRectangle, startPoint.Y);
            Canvas.SetBottom(newRectangle, startPoint.X);
            fractalCanvas.Children.Add(newRectangle);
        }

        private void fractalCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (newRectangle != null)
            {
                Point pt = e.GetPosition(fractalCanvas);

                Canvas.SetTop(newRectangle, Math.Min(pt.Y, startPoint.Y));
                Canvas.SetLeft(newRectangle, Math.Min(pt.X, startPoint.X));
                newRectangle.Width = Math.Abs(pt.X - startPoint.X);

                //Always a Square
                newRectangle.Height = Math.Abs(pt.X - startPoint.X);
            }
        }

        private void fractalCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point selectedAreaStartPoint = new Point(Canvas.GetLeft(newRectangle), Canvas.GetTop(newRectangle));

            Point selectedAreaEndPoint = new Point(
             selectedAreaStartPoint.X + newRectangle.Width,
             selectedAreaStartPoint.Y + newRectangle.Height
             );

            Point selectedAreaCentrePoint = new Point(
             selectedAreaStartPoint.X + (newRectangle.Width / 2),
             selectedAreaStartPoint.Y + (newRectangle.Height / 2)
              );

            Point newOrigin = new Point(
                selectedAreaStartPoint.X,
                selectedAreaStartPoint.Y
                );

            Canvas.SetLeft(newRectangle, selectedAreaStartPoint.X);
            Canvas.SetTop(newRectangle, selectedAreaStartPoint.Y);

            CreateNextImage(selectedAreaStartPoint);



            //Removes the drawn rectangle from Canvas's Children so it for the next fractal state
            fractalCanvas.Children.Remove(newRectangle);



        }

        private void CreateNextImage(Point _selectedAreaStartPoint)
        {
            if (storage.Choice == "Decimal")
            {
                decimal unitsPerPixel = ((dynamic)viewStateDecimal.MaxA - viewStateDecimal.MinA) / (decimal)fractalImage.Width;

                //By the book
                decimal nextStateMinA = viewStateDecimal.MinA + ((dynamic)(decimal)_selectedAreaStartPoint.X * unitsPerPixel);
                decimal nextStateMaxA = nextStateMinA + ((dynamic)(decimal)newRectangle.Width * unitsPerPixel);
                decimal nextStateMinB = viewStateDecimal.MaxB - ((dynamic)(decimal)newRectangle.Height * unitsPerPixel);
                decimal nextStateMaxB = viewStateDecimal.MaxB - ((dynamic)(decimal)_selectedAreaStartPoint.Y * unitsPerPixel);

                viewStateDecimal = new FractalFactoryViewState<decimal>();

                viewStateDecimal.CreateNextState(nextStateMaxA, nextStateMaxB, nextStateMinA, nextStateMinB, _maxIterations, _colorPalette, Convert.ToInt32(fractalImage.Width), Convert.ToInt32(fractalImage.Height));
                viewStateListDecimal.Add(viewStateDecimal);
                viewStateListCounter++;

                AwaitCreateImage(viewStateDecimal);
            }
            else
            {
                double unitsPerPixel = ((dynamic)viewStateDouble.MaxA - viewStateDouble.MinA) / fractalImage.Width;

                //By the book
                double nextStateMinA = viewStateDouble.MinA + ((dynamic)_selectedAreaStartPoint.X * unitsPerPixel);
                double nextStateMaxA = nextStateMinA + ((dynamic)newRectangle.Width * unitsPerPixel);
                double nextStateMinB = viewStateDouble.MaxB - ((dynamic)newRectangle.Height * unitsPerPixel);
                double nextStateMaxB = viewStateDouble.MaxB - ((dynamic)_selectedAreaStartPoint.Y * unitsPerPixel);

                viewStateDouble = new FractalFactoryViewState<double>();

                viewStateDouble.CreateNextState(nextStateMaxA, nextStateMaxB, nextStateMinA, nextStateMinB, _maxIterations, _colorPalette, Convert.ToInt32(fractalImage.Width), Convert.ToInt32(fractalImage.Height));
                viewStateListDouble.Add(viewStateDouble);
                viewStateListCounter++;

                AwaitCreateImage(viewStateDouble);
            }

            LoadBitMap();

            RefreshBtnBackBtnForwardOpacity();
        }

        private void BtnBack_ClickEvent(object sender, RoutedEventArgs e)
        {
            if (viewStateListCounter > 0)
            {
                viewStateListCounter--;
                if (storage.Choice == "Decimal")
                {
                    viewStateDecimal = viewStateListDecimal[viewStateListCounter];
                    AwaitCreateImage(viewStateDecimal);
                }
                else
                {
                    viewStateDouble = viewStateListDouble[viewStateListCounter];
                    AwaitCreateImage(viewStateDouble);
                }
            }
            RefreshBtnBackBtnForwardOpacity();
        }

        private void BtnForward_ClickEvent(object sender, RoutedEventArgs e)
        {


            if (storage.Choice == "Decimal")
            {
                if (viewStateListCounter < viewStateListDecimal.Count - 1)
                {
                    viewStateListCounter++;
                    viewStateDecimal = viewStateListDecimal[viewStateListCounter];
                    AwaitCreateImage(viewStateDecimal);
                }
            }
            else
            {
                if (viewStateListCounter < viewStateListDouble.Count - 1)
                {
                    viewStateListCounter++;
                    viewStateDouble = viewStateListDouble[viewStateListCounter];
                    AwaitCreateImage(viewStateDouble);
                }
            }

            RefreshBtnBackBtnForwardOpacity();
        }

        private void Btn_SaveStartCoordinates_Click(object sender, RoutedEventArgs e)
        {
            SaveViewStateAsTxt(0);
        }

        private void Btn_SaveCurrentCoordinates_Click(object sender, RoutedEventArgs e)
        {
            SaveViewStateAsTxt(viewStateListCounter);
        }

        private void SaveViewStateAsTxt(int StateCount)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string applicationFolder = "Fractal Saves";
            //Gets the path for the myDocuments path for whatever computer this program is on.
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //combines these paths
            string applicationPath = System.IO.Path.Combine(myDocuments, applicationFolder, "Generated Fractal (.txt)");
            //Sees if this path already exists
            if (!Directory.Exists(applicationPath))
            {
                Directory.CreateDirectory(applicationPath);
            }
            //All this provides the usual "Save As" function of any microsoft application.
            saveFileDialog.Filter = "txt (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.Title = "Save Fractal text File";
            saveFileDialog.InitialDirectory = applicationPath;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = "txt";

            //dialogResult can be true, false or null (just in case the dialog returns null if it is closed, which would otherwise chrash the program at this line)
            bool? dialogResult = saveFileDialog.ShowDialog();

            //If what the user enters is Save (OK) will save the file.
            if (dialogResult == true)
            {
                //Using is here to close the stream writer in case if forget to close it as a good example of defensive coding
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, true))
                {
                    if (storage.Choice == "Decimal")
                    {
                        writer.Write("Decimal|{0}|{1}|{2}|{3}|{4}",
                            viewStateListDecimal[StateCount].MaxA,
                            viewStateListDecimal[StateCount].MaxB,
                            viewStateListDecimal[StateCount].MinA,
                            viewStateListDecimal[StateCount].MinB,
                            viewStateListDecimal[StateCount].MaxIterations);
                        foreach (Color c in viewStateListDecimal[0].ColorPalette)
                        {
                            writer.Write("|{0}|{1}|{2}|{3}", c.A, c.R, c.G, c.B);
                        }
                    }
                    else
                    {
                        writer.Write("Double|{0}|{1}|{2}|{3}|{4}",
                            viewStateListDouble[StateCount].MaxA,
                            viewStateListDouble[StateCount].MaxB,
                            viewStateListDouble[StateCount].MinA,
                            viewStateListDouble[StateCount].MinB,
                            viewStateListDouble[StateCount].MaxIterations);
                        foreach (Color c in viewStateListDouble[0].ColorPalette)
                        {
                            writer.Write("|{0}|{1}|{2}|{3}", c.A, c.R, c.G, c.B);
                        }
                    }

                }
            }
        }

        private void Btn_Restart_Click(object sender, RoutedEventArgs e)
        {
            if (storage.Choice == "Decimal")
            {
                viewStateDecimal = viewStateListDecimal[0];
                AwaitCreateImage(viewStateDecimal);
            }
            else
            {
                viewStateDouble = viewStateListDouble[0];
                AwaitCreateImage(viewStateDouble);
            }
            RefreshBtnBackBtnForwardOpacity();
        }

        #region Buttons Old Code
        //private void MainForm_Load(object sender, EventArgs e)
        //{
        //    _bitmap = new Bitmap(bitmapWidth, bitmapHeight);
        //}

        //private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    minA = -2.0m;
        //    maxA = 2.0m;
        //    minB = -2.0m;
        //    maxB = 2.0m;
        //    _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
        //    picMandelbrot.Image = _bitmap;
        //}

        //private void saveAsOriginalToolStripMenuItem_Click(object sender, EventArgs e)//Saves colour scheme at original 4X4 scale
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
        //    saveFileDialog.FileName = "ColorPaletteFile(ORIG)(0)";
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");

        //    int i = 0;
        //    bool Exists = true;
        //    while (Exists == true)
        //    {
        //        if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("SaveAsStart", string.Format("ColorPaletteFile(ORIG)({0})", i)))) == true)
        //        {
        //            i++;
        //            saveFileDialog.FileName = string.Format("ColorPaletteFile(ORIG)({0})", i);
        //        }
        //        else
        //        {
        //            Exists = false;
        //        }
        //    }

        //    saveFileDialog.ShowDialog();
        //    using (StreamWriter file = new StreamWriter(saveFileDialog.FileName))
        //    {
        //        foreach (Color c in _colorPalette)
        //        {
        //            string line = String.Format("{0},{1},{2},{3}", c.A, c.R, c.G, c.B);
        //            file.WriteLine(line);
        //        }
        //        file.Write(string.Format("!-2.0,2.0,-2.0,2.0"));
        //    }
        //}

        //private void saveCurrentAsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
        //    saveFileDialog.FileName = "ColorPaletteFile(ZOOMED)(0)";
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");

        //    int i = 0;
        //    bool Exists = true;
        //    while (Exists == true)
        //    {
        //        if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine("SaveAsStart", string.Format("ColorPaletteFile(ZOOMED)({0})", i)))) == true)
        //        {
        //            i++;
        //            saveFileDialog.FileName = string.Format("ColorPaletteFile(ZOOMED)({0})", i);
        //        }
        //        else
        //        {
        //            Exists = false;
        //        }
        //    }

        //    saveFileDialog.ShowDialog();
        //    using (StreamWriter file = new StreamWriter(saveFileDialog.FileName))
        //    {
        //        foreach (Color c in _colorPalette)
        //        {
        //            string line = String.Format("{0},{1},{2},{3}", c.A, c.R, c.G, c.B);
        //            file.WriteLine(line);
        //        }
        //        file.Write(string.Format("!{0},{1},{2},{3}", minA, maxA, minB, maxB));
        //    }
        //}

        //private void openToolStripMenuItem_Click(object sender, EventArgs e)//Opens colour scheme at current place (zoomed)
        //{
        //    OpenFileDialog openTxt = new OpenFileDialog();
        //    openTxt.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");



        //    if (openTxt.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart", openTxt.FileName);
        //        _colorPalette = ColorPaletteFactory.GetColorPaletteFromFile(filePath);

        //        _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);

        //        picMandelbrot.Image = _bitmap;
        //    }
        //}

        //private void openAtOriginalPlaceToolStripMenuItem_Click(object sender, EventArgs e)//Opens colour scheme at original 4X4 scale
        //{
        //    OpenFileDialog openTxt = new OpenFileDialog();
        //    openTxt.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");

        //    if (openTxt.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart", openTxt.FileName);
        //        _colorPalette = ColorPaletteFactory.GetColorPaletteFromFile(filePath);
        //        _bitmap = Mandelbrot.CreateImage(-2.0m, 2.0m, -2.0m, 2.0m, _maxIterations, _colorPalette, _bitmap);
        //        picMandelbrot.Image = _bitmap;
        //    }
        //}

        //private void openAtFilesPlaceToolStripMenuItem_Click(object sender, EventArgs e)//Opens colour scheme at the file's placement
        //{
        //    OpenFileDialog openTxt = new OpenFileDialog();
        //    openTxt.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");
        //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart");



        //    if (openTxt.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveAsStart", openTxt.FileName);

        //        _colorPalette = ColorPaletteFactory.GetColorPaletteFromFile(filePath);
        //        List<Decimal> _coords = ColorPaletteFactory.GetCoordinates(filePath);

        //        minA = _coords[0];
        //        maxA = _coords[1];
        //        minB = _coords[2];
        //        maxB = _coords[3];
        //        _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
        //        picMandelbrot.Image = _bitmap;
        //    }
        //}

        //private void convertCurrentToBitmapToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
        //    string blankFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fractal_Blank.png");


        //    if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitmaps", "Fractal_Blank.png")))
        //    {
        //        File.Copy(blankFilePath, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitmaps", "Fractal_Blank.png"));
        //    }

        //    _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
        //    saveFileDialog.Filter = "PNG Files (*.png)|*.png|All Files (*.*)|*.*";
        //    saveFileDialog.FilterIndex = 0;
        //    SaveFileDialog savefiledialog = new SaveFileDialog();
        //    saveFileDialog.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitmaps");
        //    saveFileDialog.FileName = "NewFractal";
        //    DialogResult dialogResult = saveFileDialog.ShowDialog();
        //    string saveFilePath = saveFileDialog.FileName;

        //    string test = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bitmaps", saveFilePath + ".png");

        //    _bitmap.Save(test);

        //}

        //private void newFractalToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    _bitmap = new Bitmap(bitmapWidth, bitmapHeight);
        //    _colorPalette = GetColorPalette();
        //    if (_colorPalette != null)
        //    {
        //        minA = -2.0m;
        //        maxA = 2.0m;
        //        minB = -2.0m;
        //        maxB = 2.0m;
        //        _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
        //    }
        //    picMandelbrot.Image = _bitmap;
        //}

        //private void restartThisColourPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    _bitmap = new Bitmap(bitmapWidth, bitmapHeight);

        //    minA = -2.0m;
        //    maxA = 2.0m;
        //    minB = -2.0m;
        //    maxB = 2.0m;
        //    _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);

        //    picMandelbrot.Image = _bitmap;
        //}

        //private void MainForm_Shown(object sender, EventArgs e)
        //{
        //    _colorPalette = GetColorPalette();
        //    if (_colorPalette != null)
        //    {
        //        _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
        //        picMandelbrot.Image = _bitmap;
        //    }
        //}

        // public void UpdateProgressBar(int i)
        //{
        //    progressBar.Value = i;
        //}
        //private List<Color> GetColorPalette()
        //{
        //    minA = -2.0m;
        //    maxA = 2.0m;
        //    minB = -2.0m;
        //    maxB = 2.0m;

        //    ColorPaletteDialog colorPaletteDialog = new ColorPaletteDialog();
        //    DialogResult dialogResult = colorPaletteDialog.ShowDialog();
        //    if (dialogResult == DialogResult.OK)
        //    {
        //        return colorPaletteDialog.ColorPalette;
        //    }
        //    return null;
        //}

        //private void picMandelbrot_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (!_isDrawing)
        //    {
        //        _selectedArea.Start(e.X, e.Y);
        //        _isRubberBand = true;
        //    }
        //}

        //private void picMandelbrot_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (!_isDrawing)
        //    {

        //        if (_isRubberBand)
        //        {
        //            _selectedArea.Move(e.X, e.Y);
        //        }
        //    }
        //}

        //private void picMandelbrot_MouseUp(object sender, MouseEventArgs e)
        //{



        //    if (_isRubberBand)
        //    {
        //        if (!_isDrawing)
        //        {
        //            _selectedArea.Stop();
        //            _isRubberBand = false;

        //            //Make a square the length of the longest selected side
        //            Rectangle newRectangle = new Rectangle();
        //            if (_selectedArea.SelectedRectangle.Width > _selectedArea.SelectedRectangle.Height)
        //            {
        //                newRectangle.Width = _selectedArea.SelectedRectangle.Width;
        //                newRectangle.Height = _selectedArea.SelectedRectangle.Width;
        //            }
        //            else
        //            {
        //                newRectangle.Height = _selectedArea.SelectedRectangle.Height;
        //                newRectangle.Width = _selectedArea.SelectedRectangle.Height;
        //            }

        //            //CreateNewView();
        //            Point selectedRectangleCentrePoint = new Point();
        //            selectedRectangleCentrePoint.X = _selectedArea.SelectedRectangle.X + (_selectedArea.SelectedRectangle.Width / 2);
        //            selectedRectangleCentrePoint.Y = _selectedArea.SelectedRectangle.Y + (_selectedArea.SelectedRectangle.Height / 2);

        //            //Shift so that the square is centred on the centre of the rectangle
        //            Point newOrigin = new Point();
        //            newOrigin.X = selectedRectangleCentrePoint.X - (newRectangle.Width / 2);
        //            newOrigin.Y = selectedRectangleCentrePoint.Y - (newRectangle.Height / 2);
        //            newRectangle.Location = newOrigin;

        //            //Convert into plot values
        //            decimal unitsPerPixel = (maxA - minA) / bitmapWidth;
        //            minA = minA + (newRectangle.X * unitsPerPixel);
        //            maxA = minA + (newRectangle.Width * unitsPerPixel);
        //            minB = maxB - (newRectangle.Height * unitsPerPixel);
        //            maxB = maxB - (newRectangle.Y * unitsPerPixel);

        //            //EventArgs eventArg = new EventArgs();
        //            //eventArg += p_Progress;
        //            //Process p = new Process();
        //            //p. += p_Progress;


        //            _bitmap = Mandelbrot.CreateImage(minA, maxA, minB, maxB, _maxIterations, _colorPalette, _bitmap);
        //            picMandelbrot.Image = _bitmap;

        //        }
        //    }
        //}
        #endregion

        private void Btn_OpenStartCoordinates_Click(object sender, RoutedEventArgs e)
        {
            viewStateDecimal = null;
            viewStateDouble = null;
            viewStateListDecimal = null;
            viewStateListDouble = null;
            viewStateListCounter = 0;
            mandelbrotDecimal = null;
            mandelbrotDouble = null;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                string applicationFolder = "Fractal Saves";
                //Gets the path for the myDocuments path for whatever computer this program is on.
                string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //combines these paths
                string applicationPath = System.IO.Path.Combine(myDocuments, applicationFolder, "Generated Fractal (.txt)");
                //Sees if this path already exists
                if (!Directory.Exists(applicationPath))//?needed?
                {
                    Directory.CreateDirectory(applicationPath);
                }
                //All this provides the usual "Save As" function of any microsoft application.
                openFileDialog.Filter = "txt (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.Title = "Open Fractal text File";
                openFileDialog.InitialDirectory = applicationPath;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckFileExists = false;
                openFileDialog.CheckPathExists = true;
                openFileDialog.DefaultExt = "txt";

                //dialogResult can be true, false or null (just in case)
                bool? dialogResult = openFileDialog.ShowDialog();

                //If what the user enters is Open (OK) will open the file.
                if (dialogResult == true)
                {
                    string filename = openFileDialog.FileName;

                    using (StreamReader reader = new StreamReader(filename, true))
                    {

                        string[] splitter = reader.ReadLine().Split('|');

                        if (splitter[0] == "Decimal")
                        {
                            viewStateDecimal = new FractalFactoryViewState<decimal>();
                            viewStateListDecimal = new List<FractalFactoryViewState<decimal>>();
                            mandelbrotDecimal = new Mandelbrot<decimal>();

                            Color[] colors = new Color[(splitter.Count() - 6) / 4];
                            for (int i = 0; i < splitter.Count() - 8; i++)
                            {
                                colors[i] = Color.FromArgb(
                                    Convert.ToByte(splitter[6 + i]),
                                    Convert.ToByte(splitter[7 + i]),
                                    Convert.ToByte(splitter[8 + i]),
                                    Convert.ToByte(splitter[9 + i]));
                            }


                            viewStateDecimal.CreateNextState(
                                Convert.ToDecimal(splitter[1]),
                                Convert.ToDecimal(splitter[2]),
                                Convert.ToDecimal(splitter[3]),
                                Convert.ToDecimal(splitter[4]),
                               Convert.ToInt32(splitter[5]),
                                _colorPalette,
                                Convert.ToInt32(fractalImage.Width),
                                Convert.ToInt32(fractalImage.Height));
                            viewStateListDecimal.Add(viewStateDecimal);
                            viewStateListCounter++;

                            //mandelbrotDecimal.CreateImage(viewStateDecimal);
                            LoadBitMap();
                        }
                        else
                        {
                            viewStateDouble = new FractalFactoryViewState<double>();
                            viewStateListDouble = new List<FractalFactoryViewState<double>>();
                            mandelbrotDouble = new Mandelbrot<double>();
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
