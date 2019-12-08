using Roguelike.EventArgsClases;
using SFML.Graphics;
using System;

namespace Roguelike.Pages
{
    interface Page : Drawable
    {
        event EventHandler<ChangedPageEventArgs> ChangedPage;
        event EventHandler<ChangedViewCenterEventArgs> ChangedViewCenter;
    }
}
