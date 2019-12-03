using Roguelike.EventArgsClases;
using Roguelike.GamePlayComponents;
using SFML.Graphics;
using System;

namespace Roguelike.Pages
{
    class GamePlay : Page
    {
        public event EventHandler<ChangedPageEventArgs> ChangedPage;

        private UserInterface UserInterface;

        private GameField GameField;

        private Pause Pause;

        public GamePlay()
        {
            UserInterface = new UserInterface();
            UserInterface.GameOver += EndTheGame;
            GameField = new GameField();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                target.Draw(GameField, states);
                target.Draw(UserInterface, states);
                if (Pause != null)
                    target.Draw(Pause, states);
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EndTheGame(object sender, EndGameEventArgs e)
        {
            ChangedPage.Invoke(this, new ChangedPageEventArgs(new MainMenu()));
        }
    }
}
