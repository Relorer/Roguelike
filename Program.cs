using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike
{
    class Program
    {
        private static RenderWindow Window;
        private static bool FullScreen;

        public static void Main(string[] args)
        {
            Window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width / 2, VideoMode.DesktopMode.Height / 2), "Roguelike", Styles.Default);
            FullScreen = false;
            ToConfigureWindow();
            Open();
        }

        private static void ToConfigureWindow()
        {
            Window.SetKeyRepeatEnabled(false);
            Window.SetVerticalSyncEnabled(true);
            Window.SetFramerateLimit(60);

            Window.Closed += Window_Closed;
            Window.Resized += Window_Resized;
            Window.KeyPressed += Window_KeyPressed;
        }

        private static void Open()
        {
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                Window.Display();
            }
        }

        private static void TryToChangeDisplayMode()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.LAlt) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                Window.Close();
                if (FullScreen)
                {
                    Window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width / 2, VideoMode.DesktopMode.Height / 2), "Roguelike");
                }
                else
                {
                    Window = new RenderWindow(new VideoMode(), "Roguelike", Styles.Fullscreen);
                }
                FullScreen = !FullScreen;
            }
            ToConfigureWindow();
        }

        private static void Window_Resized(object sender, SizeEventArgs e)
        {
            Window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private static void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            TryToChangeDisplayMode();
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
