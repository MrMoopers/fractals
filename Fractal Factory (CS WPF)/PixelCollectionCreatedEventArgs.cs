using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Fractal_Factory_V2._0
{
    public class PixelCollectionCreatedEventArgs : EventArgs
    {
        private List<Color> _colors;
        private int _offset;

        public PixelCollectionCreatedEventArgs(List<Color> colors, int offset)
        {
            _colors = colors;
            _offset = offset;

        }

        public List<Color> Colors
        {
            get { return _colors; }
        }
        public int Offset
        {
            get { return _offset; }
        }
    }
}