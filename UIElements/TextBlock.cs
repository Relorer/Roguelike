using Roguelike.PropertyClasses;
using SFML.Graphics;
using SFML.System;

namespace Roguelike.UIElements
{
    class TextBlock : UIElement
    {
        public Vector2f Position {
            get {
                return Text.Position;
            }
            set {
                Text.Position = value;
            }
        }

        public Margin Margin { get; set; }

        public string DisplayedString {
            get {
                return Text.DisplayedString;
            }
            set {
                Text.DisplayedString = value;
            }
        }

        public State State {
            get {
                return state;
            }
            set {
                state = value;
                switch (value)
                {
                    case State.Free:
                        Text.FillColor = new Color(200, 200, 200);
                        break;
                    case State.Hovered:
                        Text.FillColor = new Color(220, 220, 220);
                        break;
                    case State.Clamped:
                        Text.FillColor = new Color(255, 255, 255);
                        break;
                    case State.Clicked:
                        Text.FillColor = new Color(255, 255, 255);
                        break;
                }
            }
        }

        private State state;

        private Text Text;

        public TextBlock(string text)
        {
            Text = new Text(text, Program.Font);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Text);
        }

        public FloatRect GetBounds()
        {
            return Text.GetGlobalBounds();
        }
    }
}
