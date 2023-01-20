using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalsCS
{
    public static class Mandelbrot
    {

        public static Bitmap CreateImage(double minA, double maxA, double minB, double maxB, int maxIterations, List<Color> colorPalette, Bitmap bmpMandel)
        {
            double unitsPerPixel = (maxA - minA) / bmpMandel.Width;
            for (int y = 0; y < bmpMandel.Height; y++)
            {
                for (int x = 0; x < bmpMandel.Width; x++)
                {
                    double a = minA + (x * unitsPerPixel);
                    double b = maxB - (y * unitsPerPixel);
                    int numIterations = IsInMandelbrotSet(a, b, maxIterations);
                    Color pixelC = Color.Black;
                    if (numIterations < maxIterations)
                    {

                        double percentIterations = (double)numIterations / (double)maxIterations;
                        int colorPaletteIndex = (int)((colorPalette.Count - 1) * percentIterations);
                        pixelC = colorPalette[numIterations % (colorPalette.Count - 1)];
                        //pixelC = colorPalette[numIterations % 25];
                        //Debug.WriteLine(colorPaletteIndex);

                    }
                    bmpMandel.SetPixel(x, y, pixelC);
                }
            }
            return bmpMandel;
        }

        private static int IsInMandelbrotSet(double a, double b, int maxIterations)
        {
            double startA = a;
            double startB = b;
            int numIterations = 0;
            while (numIterations < maxIterations)
            {
                double aSquare = a * a;
                double bSquare = b * b;
                double squareOfDistance = aSquare + bSquare;
                if (squareOfDistance > 4)
                {
                    return numIterations;
                }
                b = 2 * a * b + startB;
                a = (aSquare - bSquare) + startA;
                numIterations++;
            }
            return numIterations;
        }
    }




}
