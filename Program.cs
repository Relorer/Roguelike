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
        public static RenderWindow Window { get; private set; }

        public static float AspectRatio {
            get {
                return (float)Settings.Default.Resolution.X / Settings.Default.Resolution.Y;
            }
        }

        public static Font Font { get; private set; }

        private static float Scale;

        private static Page Page;

        public static void Main(string[] args)
        {
            Window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width / 2, VideoMode.DesktopMode.Height / 2), "Roguelike", Settings.Default.ScreenMode);
            ToConfigureWindow();
            Font = new Font("Resources\\Fonts\\Casale.ttf");
            Program_ChangedPage(null, new ChangedPageEventArgs(new MainMenu()));
            Open();
            Settings.Default.Save();
        }

        private static void ToConfigureWindow()
        {
            Window.SetKeyRepeatEnabled(false);
            Window.SetVerticalSyncEnabled(true);
            Window.SetFramerateLimit(60);
            ToChangeView();

            Window.Closed += Window_Closed;
            Window.Resized += Window_Resized;
            Window.KeyPressed += Window_KeyPressed;
            Window.KeyReleased += Window_KeyReleased;
        }

        private static void Open()
        {
            StaticClock.Restart();
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear();
                RenderStates states = RenderStates.Default;
                states.Transform.Scale(Scale, Scale);
                Window.Draw(Page, states);
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

        private static void ToChangeView()
        {
            var res = Settings.Default.Resolution;
            View view = new View(new Vector2f(res.X / 2, res.Y / 2), new Vector2f(res.X, res.Y));
            Vector2f size = (Vector2f)Window.Size;
            if (size.X / size.Y > AspectRatio)
            {
                float width = size.Y * AspectRatio / size.X;
                view.Viewport = new FloatRect((1 - width) / 2, 0, width, 1);
            }
            else
            {
                float height = size.X / AspectRatio / size.Y;
                view.Viewport = new FloatRect(0, (1 - height) / 2, 1, height);
            }
            Window.SetView(view);
            Scale = Settings.Default.Resolution.X * 0.002f;
        }

        private static void Program_ChangedPage(object sender, ChangedPageEventArgs e)
        {
            Page = e.NewPage;
            Page.ChangedPage += Program_ChangedPage;
            Page.ChangedViewCenter += Page_ChangedViewCenter;
        }

        private static void Page_ChangedViewCenter(object sender, ChangedViewCenterEventArgs e)
        {
            View view = Window.GetView();
            view.Center = new Vector2f(e.Center.X, e.Center.Y) * Scale;
            Window.SetView(view);
        }

        private static void Window_Resized(object sender, SizeEventArgs e)
        {
            uint height = e.Height < 400 ? 400 : e.Height;
            uint width = e.Width < 600 ? 600 : e.Width;
            Window.Size = new Vector2u(width, height);
            ToChangeView();
        }

        private static void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.LAlt) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                ToChangeDisplayMode();
            }
        }

        private static void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape && Page is GamePlay)
            {
                var gp = Page as GamePlay;
                gp.IsPause = !gp.IsPause;
            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}