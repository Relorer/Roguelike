using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.EventArgsClases
{
    class ScrollChangedEventArgs : EventArgs
    {
        public float SliderPosition { get; private set; }
       
        public ScrollChangedEventArgs(float sliderPosition)
        {
            SliderPosition = sliderPosition;
        }
    }
}
