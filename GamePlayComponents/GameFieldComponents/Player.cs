using Roguelike.Animations;
using SFML.Graphics;
using SFML.System;
using System;

namespace Roguelike.GamePlayComponents.GameFieldComponents
{
    class Player : Drawable
    {
        private AnimationManager AnimationManager;

        public Vector2f Position { get; private set; }

        public Vector2f DeltaPosition {
            set {
                Position += value;
            }
        }

        public float Speed { get; private set; }

        public Player()
        {
            Speed = 0.2f;
            AnimationManager = new AnimationManager();
            AnimationManager.Add(new Animation(new Texture("Resources\\Textures\\Heroes\\Coub\\Coub.png"), new Vector2i(0, 0), new Vector2i(16, 16), 6, 1300), "AFK");
            AnimationManager.Current = "AFK";
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(AnimationManager.GetSprite(Position), states);
        }
    }
}