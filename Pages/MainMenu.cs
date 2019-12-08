using Roguelike.EventArgsClases;
using Roguelike.Properties;
using Roguelike.PropertyClasses;
using Roguelike.UIElements;
using SFML.Graphics;
using SFML.System;
using System;

namespace Roguelike.Pages
{
    class MainMenu : Page
    {
        public event EventHandler<ChangedPageEventArgs> ChangedPage;

        public event EventHandler<ChangedViewCenterEventArgs> ChangedViewCenter;

        private StackPanel Buttons;

        public MainMenu()
        {
            Buttons = new StackPanel();
            Buttons.HorizontalAlignment = HorizontalAlignment.Center;
            AddButton("Start");
            AddButton("Options");
            AddButton("Exit");
            var res = Settings.Default.Resolution;
            Buttons.Position = new Vector2f((res.X - Buttons.GetBounds().Width) / 2, (res.Y - Buttons.GetBounds().Height) / 2);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Buttons);
        }

        private void AddButton(string title)
        {
            Button button = new Button(new TextBlock(title), title);
            button.Margin = new Margin(20);
            button.Click += Button_Click;
            Buttons.Add(button);
        }

        private void Button_Click(object sender, ButtonClickEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "Start":
                    ChangedPage.Invoke(this, new ChangedPageEventArgs(new GamePlay()));
                    break;
                case "Options":
                    ChangedPage.Invoke(this, new ChangedPageEventArgs(new Options()));
                    break;
                case "Exit":
                    Program.Window.Close();
                    break;
            }
        }
    }
}
