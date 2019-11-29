using Roguelike.Properties;
using SFML.Graphics;
using SFML.Window;
using System;

namespace Roguelike
{
    class Program
    {
        private static RenderWindow Window;

        public static void Main(string[] args)
        {
            Window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width / 2, VideoMode.DesktopMode.Height / 2), "Roguelike", Settings.Default.ScreenMode);
            ToConfigureWindow();
            Open();
            Settings.Default.Save();
        }

        private static void ToConfigureWindow()
        {
            Window.SetKeyRepeatEnabled(false);
            Window.SetVerticalSyncEnabled(true);
            Window.SetFramerateLimit(60);
            Image icon = new Image("Resources\\icon.png");
            Window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);

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

        private static void ToChangeDisplayMode()
        {
            Window.Close();
            Settings.Default.ScreenMode = Settings.Default.ScreenMode == Styles.Default ? Styles.Fullscreen : Styles.Default;
            Window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width / 2, VideoMode.DesktopMode.Height / 2), "Roguelike", Settings.Default.ScreenMode);
            ToConfigureWindow();
        }

        private static void Window_Resized(object sender, SizeEventArgs e)
        {
            Window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private static void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.LAlt) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                ToChangeDisplayMode();
            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
