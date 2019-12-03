using Roguelike.Pages;
using System;

namespace Roguelike.EventArgsClases
{
    class ChangedPageEventArgs : EventArgs
    {
        public Page NewPage { get; private set; }

        public ChangedPageEventArgs(Page newPage)
        {
            NewPage = newPage;
        }
    }
}
