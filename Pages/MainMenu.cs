using Roguelike.EventArgsClases;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Pages
{
    class MainMenu : Page
    {
        public event EventHandler<ChangedPageEventArgs> ChangedPage;

        public void Draw(RenderTarget target, RenderStates states)
        {
            ChangedPage.Invoke(this, new ChangedPageEventArgs(new GamePlay()));
        }
    }
}
