using Roguelike.EventArgsClases;
using Roguelike.Extensions;
using Roguelike.PropertyClasses;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Roguelike.UIElements
{
    class Button : UIElement
    {
        public event EventHandler<ButtonClickEventArgs> Click;

        public Vector2f Position {
            get {
                return Content.Position;
            }
            set {
                Content.Position = value;
            }
        }

        public State State {
            get {
                return Content.State;
            }
            set {
                Content.State = value;
                if (value == State.Clicked)
                    Click.Invoke(this, new ButtonClickEventArgs());
            }
        }

        public Margin Margin { get; set; }

        public string Name { get; set; }

        private UIElement Content;

        public Button(UIElement content, string name = "")
        {
            Content = content;
            Name = name;
        }

        public FloatRect GetBounds()
        {
            return Content.GetBounds();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            State = GetState();
            target.Draw(Content, states);
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
            return Content.GetBounds().Contains(pos.X, pos.Y);
        }
    }
}