using Roguelike.EventArgsClases;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.GamePlayComponents
{
    class UserInterface : Drawable
    {
        public event EventHandler<EndGameEventArgs> GameOver;

        public void Draw(RenderTarget target, RenderStates states)
        {
            //todo
        }
    }
}
