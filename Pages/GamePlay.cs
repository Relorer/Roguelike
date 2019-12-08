using Roguelike.EventArgsClases;
using Roguelike.GamePlayComponents;
using SFML.Graphics;
using System;

namespace Roguelike.Pages
{
    class GamePlay : Page
    {
        public event EventHandler<ChangedPageEventArgs> ChangedPage;

        public event EventHandler<ChangedViewCenterEventArgs> ChangedViewCenter;

        private UserInterface UserInterface;

        private GameField GameField;

        private Pause Pause;

        public bool IsPause {
            get {
                return Pause != null;
            }
            set {
                Pause = value ? new Pause() : null;
            }
        }

        public GamePlay()
        {
            UserInterface = new UserInterface();
            UserInterface.GameOver += EndTheGame;
            GameField = new GameField();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ChangedViewCenter.Invoke(this, new ChangedViewCenterEventArgs(GameField.Player.Position));
            target.Draw(GameField, states);
            target.Draw(UserInterface, states);
            if (IsPause)
                target.Draw(Pause, states);
        }

        private void EndTheGame(object sender, EndGameEventArgs e)
        {
            ChangedPage.Invoke(this, new ChangedPageEventArgs(new MainMenu()));
        }
    }
}
