using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.EventArgsClases
{
    class ChangedViewCenterEventArgs : EventArgs
    {
        public Vector2f Center { get; private set; }

        public ChangedViewCenterEventArgs(Vector2f center)
        {
            Center = center;
        }
    }
}
