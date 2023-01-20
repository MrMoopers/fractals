using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractal_Factory_V2._0
{
    class FractalDisplayBase
    {

        //Event handlers for each event: wall created, passage created or the routepath getting a new cell added
        public event EventHandler<PixelCreatedEventArgs> RaisePixelCreatedEvent;
        public event EventHandler<PixelCollectionCreatedEventArgs> RaisePixelCollectionCreatedEvent;
        //private int count = 0;
        //For raising a new pixel created event arg
        protected virtual void OnRaisePixelCreatedEvent(PixelCreatedEventArgs e)
        {
            //if (count % 2 == 0)
            //{
                RaisePixelCreatedEvent?.Invoke(this, e);
            //}
            //count++;
        }

        protected virtual void OnRaisePixelCollectionCreatedEvent(PixelCollectionCreatedEventArgs e)
        {
            //if (count % 2 == 0)
            //{
            RaisePixelCollectionCreatedEvent?.Invoke(this, e);
            //}
            //count++;
        }
    }
}
