using Roguelike.EventArgsClases;
using Roguelike.Extensions;
using Roguelike.PropertyClasses;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Roguelike.UIElements
{
    class ScrollViewer : UIElement
    {
        public Vector2f Position {
            get {
                return position;
            }
            set {
                position = value;
                UIElement.Position = value;
            }
        }

        public Margin Margin { get; set; }

        public FloatRect Bounds { get; set; }
        public State State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private Vector2f position;

        private UIElement UIElement;

        private Scroll HorizontalScroll;

        private Scroll VerticalScroll;

        public ScrollViewer(UIElement uIElement)
        {
            UIElement = uIElement;
            FloatRect rect = uIElement.GetBounds();
            HorizontalScroll = new Scroll(new Vector2f(rect.Width - 20, 10), Orientation.Horizontal);
            VerticalScroll = new Scroll(new Vector2f(10, rect.Height - 20));
            Bounds = uIElement.GetBounds();
            VerticalScroll.Occupation = 0.2f;
            HorizontalScroll.Occupation = 0.2f;
            Program.Window.MouseWheelScrolled += Window_MouseWheelScrolled;
            HorizontalScroll.ScrollChanged += Scroll_ScrollChanged;
        }

        public ScrollViewer(UIElement uIElement, FloatRect bounds) : this(uIElement)
        {
            Bounds = bounds;
        }

        public FloatRect GetBounds()
        {
            return Bounds;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Bounds = new FloatRect(0, 0, 40, 40);
            states.Transform.TransformRect(new FloatRect());
            target.Draw(UIElement, states);
            target.Draw(HorizontalScroll, states);
            target.Draw(VerticalScroll, states);
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //todo
        }

        private void Window_MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            Vector2f pos = Program.Window.GetMousePositionOnViewport();
            Console.WriteLine(pos);
            Console.WriteLine(Program.Window.MapPixelToCoords(Mouse.GetPosition()));

            if (Bounds.Contains(pos.X, pos.Y))
            {
                if (e.Wheel == Mouse.Wheel.VerticalWheel)
                {
                    VerticalScroll.ToScroll(-e.Delta * 0.1f * Bounds.Height);
                }
                else
                {
                    HorizontalScroll.ToScroll(e.Delta * 0.1f * Bounds.Width);
                }
            }
        }
    }
}