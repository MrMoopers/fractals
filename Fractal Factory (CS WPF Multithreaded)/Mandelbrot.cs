using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;

namespace FractalFactory
{
    class Mandelbrot<T> : FractalDisplayBase
    {
        internal void CreateImage(FractalFactoryViewState<T> viewState, Point startPoint, Point endPoint)
        {
            var pixelFormat = PixelFormats.Pbgra32;
            int bytesPerPixel = (pixelFormat.BitsPerPixel + 7) / 8;
            var stride = bytesPerPixel * viewState.Width;

            int bufferSize = viewState.Width * viewState.Height * bytesPerPixel;
            byte[] byteArray = new byte[bufferSize];
            byte[] singlePixel = new byte[4];
            List<Color> colorCollection = new List<Color>();
            //int offset = (int)((endPoint.X - startPoint.X) * (endPoint.Y - startPoint.Y) * bytesPerPixel) ;
            int offset = (int)(((endPoint.X - startPoint.X) * (endPoint.Y - startPoint.Y) * bytesPerPixel) + ((startPoint.Y * (endPoint.X - startPoint.X)) * bytesPerPixel));

            T unitsPerPixel;
                    
            if (typeof(T) == typeof(decimal))
            {
                unitsPerPixel = ((dynamic)viewState.MaxA - viewState.MinA) / (decimal)viewState.Width;
            }
            else
            {
                unitsPerPixel = ((dynamic)viewState.MaxA - viewState.MinA) / viewState.Width;
            }

            for (int y = (int)startPoint.Y; y < (int)endPoint.Y; y++)
            {
                for (int x = (int)startPoint.X; x < (int)endPoint.X; x++)
                {
                    T a = viewState.MinA + ((dynamic)x * unitsPerPixel);
                    T b = viewState.MaxB - ((dynamic)y * unitsPerPixel);

                    int numIterations = IsInMandelbrotSet(a, b, viewState.MaxIterations);
                    Color pixelC = Colors.Black;
                    if (numIterations < viewState.MaxIterations)
                    {

                        decimal percentIterations = (decimal)numIterations / (decimal)viewState.MaxIterations;
                        int colorPaletteIndex = (int)((viewState.ColorPalette.Count - 1) * percentIterations);
                        pixelC = viewState.ColorPalette[numIterations % (viewState.ColorPalette.Count - 1)];
                    }

                    if (x == 0)
                    {
                         offset = ((y * viewState.Width) + x) * bytesPerPixel;
                        //OnRaisePixelCreatedEvent(new PixelCreatedEventArgs(pixelC, offset));
                    }
                    colorCollection.Add(pixelC);
                }

                OnRaisePixelCollectionCreatedEvent(new PixelCollectionCreatedEventArgs(colorCollection, offset));
                //Thread.Sleep(50);
                Thread.Yield();
           //     colorCollection = null;
                colorCollection = new List<Color>();
            }
            
        }

        private int IsInMandelbrotSet(T numberA, T numberB, int maxIterations)
        {
            T startA = numberA;
            T startB = numberB;
            int numIterations = 0;
            while (numIterations < maxIterations)
            {
                T aSquare = (dynamic)numberA * numberA;
                T bSquare = (dynamic)numberB * numberB;
                T squareOfDistance = (dynamic)aSquare + bSquare;
                if ((dynamic)squareOfDistance > 4)
                {
                    return numIterations;
                }
                numberB = (dynamic)2 * numberA * numberB + startB;
                numberA = ((dynamic)aSquare - bSquare) + startA;
                numIterations++;
            }
            return numIterations;

        }
    }
}
