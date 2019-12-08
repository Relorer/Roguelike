using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.EventArgsClases;
using SFML.Graphics;

namespace Roguelike.Pages
{
    class Options : Page
    {
        public event EventHandler<ChangedPageEventArgs> ChangedPage;

        public event EventHandler<ChangedViewCenterEventArgs> ChangedViewCenter;

        public void Draw(RenderTarget target, RenderStates states)
        {
            Console.WriteLine("Options");
        }
    }
}
