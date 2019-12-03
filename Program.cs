using Roguelike.EventArgsClases;
using Roguelike.Pages;
using Roguelike.Properties;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Roguelike
{
    class Program
    {
        private static RenderWindow Window;

        private static Page Page;

        public static void Main(string[] args)
        {
            Window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width / 2, VideoMode.DesktopMode.Height / 2), "Roguelike", Settings.Default.ScreenMode);
            ToConfigureWindow();
            Program_ChangedPage(null, new ChangedPageEventArgs(new MainMenu()));
            Open();
            Settings.Default.Save();
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
            StaticClock.Restart();
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                Window.Draw(Page);
                StaticClock.Restart();
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

        private static void Program_ChangedPage(object sender, ChangedPageEventArgs e)
        {
            Page = e.NewPage;
            Page.ChangedPage += Program_ChangedPage;
        }

        private static void Window_Resized(object sender, SizeEventArgs e)
        {
            Window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
            if (e.Width < 600) Window.Size = new Vector2u(600, e.Height);
            else if (e.Height < 400) Window.Size = new Vector2u(e.Width, 400);
            
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