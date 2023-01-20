using System;
using System.Windows.Media;

namespace FractalFactory
{
    public class PixelCreatedEventArgs : EventArgs
    {
        private Color _color;
        private int _offset;
        
        public PixelCreatedEventArgs(Color color, int offset)
        {
            _color = color;
            _offset = offset;
        }

        public Color Color
        {
            get { return _color; }
        }
        public int Offset
        {
            get { return _offset; }
        }
    }
}