using Roguelike.GamePlayComponents.GameFieldComponents;
using Roguelike.Properties;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Roguelike.GamePlayComponents
{
    class GameField : Drawable
    {
        public Player Player { get; private set; }

        private RectangleShape TempMap;

        public GameField()
        {
            Player = new Player();
            var res = Settings.Default.Resolution;
            TempMap = new RectangleShape(new Vector2f(100, 100));
            TempMap.FillColor = Color.Yellow;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            CalculateFrame();
            target.Draw(TempMap, states);
            target.Draw(Player, states);
        }

        private void CalculateFrame()
        {
            ProcessManagement();
        }

        private void ProcessManagement()
        {
            long time = StaticClock.ElapsedMilliseconds;
            Vector2f direction = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) direction.Y -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) direction.Y += 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) direction.X += 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) direction.X -= 1;

            float k = (Math.Abs(direction.X) + Math.Abs(direction.Y));
            k = k > 0 ? 1 / k : 1;
            direction = direction * k;

            Player.DeltaPosition = new Vector2f(Player.Speed * time * direction.X, Player.Speed * time * direction.Y);
        }
    }
}