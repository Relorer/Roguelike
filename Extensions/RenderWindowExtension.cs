using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Roguelike.Extensions
{
    public static class RenderWindowExtension
    {
        public static Vector2f GetMousePositionOnViewport(this RenderWindow window)
        {
            Vector2i posInt = Mouse.GetPosition(window);
            Vector2f position = new Vector2f(posInt.X, posInt.Y);
            View view = window.GetView();
            position -= new Vector2f(view.Viewport.Left * window.Size.X, view.Viewport.Top * window.Size.Y);
            position *= view.Size.X / view.Viewport.Width / window.Size.X;
            return position;
        }
    }
}