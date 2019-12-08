using Roguelike.EventArgsClases;
using Roguelike.Extensions;
using Roguelike.PropertyClasses;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Roguelike.UIElements
{
    class Scroll : UIElement
    {
        public event EventHandler<ScrollChangedEventArgs> ScrollChanged;

        public Orientation Orientation { get; private set; }

        public State State {
            get {
                return state;
            }
            set {
                switch (value)
                {
                    case State.Free:
                        break;
                    case State.Hovered:
                        break;
                    case State.Clamped:
                        if (state == State.Clamped)
                        {
                            Vector2f pos = Program.Window.GetMousePositionOnViewport();
                            if (Orientation == Orientation.Vertical)
                            {
                                ToScroll(pos.Y - LastPositionMouse.Y);
                            }
                            else
                            {
                                ToScroll(pos.X - LastPositionMouse.X);
                            }
                        }
                        LastPositionMouse = Program.Window.GetMousePositionOnViewport();
                        break;
                    case State.Clicked:
                        break;
                }
                state = value;
            }
        }

        public Vector2f Position { get; set; }

        public Margin Margin { get; set; }

        public Vector2f Size { get; set; }

        public float SliderPosition {
            get {
                if (Orientation == Orientation.Vertical)
                {
                    return (Slider.Position.Y - Position.Y) / Size.Y;
                }
                else
                {
                    return (Slider.Position.X - Position.X) / Size.X;
                }
            }
            set {
                if (Orientation == Orientation.Vertical)
                {
                    Slider.Position = new Vector2f(Slider.Position.X, Position.Y + Size.Y * value);
                }
                else
                {
                    Slider.Position = new Vector2f(Position.X + Size.X * value, Slider.Position.Y);
                }
            }
        }

        public float Occupation {
            get {
                return occupation;
            }
            set {
                occupation = value;
                if (Orientation == Orientation.Vertical)
                {
                    Slider.Size = new Vector2f(Slider.Size.X, occupation * Size.Y);
                }
                else
                {
                    Slider.Size = new Vector2f(occupation * Size.X, Slider.Size.Y);
                }
            }
        }

        public Color Color {
            get {
                return Slider.FillColor;
            }
            set {
                Slider.FillColor = value;
            }
        }

        private RectangleShape Slider;

        private State state;

        private float occupation;

        private Vector2f LastPositionMouse;

        public Scroll(Vector2f size, Orientation orientation = Orientation.Vertical)
        {
            Slider = new RectangleShape(size);
            Slider.FillColor = new Color(200, 200, 200);
            Size = size;
            Occupation = 1;
            Orientation = orientation;
        }

        public Scroll()
        {
        }

        public FloatRect GetBounds()
        {
            return new FloatRect(Position, Size);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            State = GetState();
            target.Draw(Slider, states);
        }

        public void ToScroll(float scroll)
        {
            if (Orientation == Orientation.Vertical)
            {
                Slider.Position = new Vector2f(Slider.Position.X, Slider.Position.Y + scroll);
            }
            else
            {
                Slider.Position = new Vector2f(Slider.Position.X + scroll, Slider.Position.Y);
            }
            if (SliderPosition > 1 - Occupation) SliderPosition = 1 - Occupation;
            else if (SliderPosition < 0) SliderPosition = 0;

            ScrollChanged?.Invoke(this, new ScrollChangedEventArgs(SliderPosition));
        }

        private State GetState()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left) && (State == State.Clamped || State == State.Hovered))
            {
                return State.Clamped;
            }
            else if (!IsHovering())
            {
                return State.Free;
            }
            else if (State == State.Clamped)
            {
                return State.Clicked;
            }
            else if (!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                return State.Hovered;
            }
            return State.Free;
        }

        private bool IsHovering()
        {
            Vector2f pos = Program.Window.GetMousePositionOnViewport();
            return Slider.GetGlobalBounds().Contains(pos.X, pos.Y);
        }
    }
}