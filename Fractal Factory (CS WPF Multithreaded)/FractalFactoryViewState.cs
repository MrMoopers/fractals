using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FractalFactory
{
    class FractalFactoryViewState<T>
    {
        public T MaxA { get; set; }
        public T MaxB { get; set; }
        public T MinA { get; set; }
        public T MinB { get; set; }
        public int MaxIterations { get; set; }
        public List<Color> ColorPalette { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        internal void CreateNextState(T maxA, T maxB, T minA, T minB, int maxIterations, List<Color> colorPalette, int width, int height)
        {
            MaxA = maxA;
            MaxB = maxB;
            MinA = minA;
            MinB = minB;
            MaxIterations = maxIterations;
            ColorPalette = colorPalette;
            Width = width;
            Height = height;
        }
    }
}
