using Roguelike.PropertyClasses;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Roguelike.UIElements
{
    class StackPanel : UIElement
    {
        public List<UIElement> Items { get; private set; }

        public Vector2f Position {
            get {
                return position;
            }
            set {
                position = value;
                ToReconstruct();
            }
        }

        public Orientation Orientation {
            get {
                return orientation;
            }
            set {
                orientation = value;
                ToReconstruct();
            }
        }

        public VerticalAlignment VerticalAlignment {
            get {
                return verticalAlignment;
            }
            set {
                verticalAlignment = value;
                ToReconstruct();
            }
        }

        public HorizontalAlignment HorizontalAlignment {
            get {
                return horizontalAlignment;
            }
            set {
                horizontalAlignment = value;
                ToReconstruct();
            }
        }

        public Margin Margin { get; set; }

        public State State { get; set; }

        private Vector2f position;

        private Orientation orientation;

        private VerticalAlignment verticalAlignment;

        private HorizontalAlignment horizontalAlignment;

        public StackPanel(Orientation orientation = Orientation.Vertical, VerticalAlignment vertical = VerticalAlignment.Top, HorizontalAlignment horizontal = HorizontalAlignment.Left)
        {
            Items = new List<UIElement>();
            Orientation = orientation;
            VerticalAlignment = vertical;
            HorizontalAlignment = horizontal;
        }

        public FloatRect GetBounds()
        {
            UIElement last = Items[Items.Count - 1];
            if (Orientation == Orientation.Vertical)
            {
                return new FloatRect(Position, new Vector2f(GetMaxWidthItem(), last.Position.Y + last.GetBounds().Height + last.Margin.Bottom - Position.Y));
            }
            else
            {
                return new FloatRect(Position, new Vector2f(last.Position.X + last.GetBounds().Width + last.Margin.Right - Position.X, GetMaxHeightItem()));
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (UIElement item in Items) target.Draw(item, states);
        }

        public void Add(UIElement item)
        {
            Items.Add(item);
            ToReconstruct();
        }

        private void ToReconstruct()
        {
            if (Items.Count == 0) return;
            if (Orientation == Orientation.Vertical)
            {
                ToReconstructVertical();
            }
            else
            {
                ToReconstructHorizon();
            }
        }

        private void ToReconstructVertical()
        {
            float maxWidth = GetMaxWidthItem();
            float y = 0;
            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    foreach (UIElement item in Items)
                    {
                        item.Position = new Vector2f(position.X, position.Y + y + item.Margin.Top);
                        y += item.GetBounds().Height + item.Margin.Bottom;
                    }
                    break;
                case HorizontalAlignment.Center:
                    foreach (UIElement item in Items)
                    {
                        float x = (maxWidth - item.GetBounds().Width) / 2;
                        item.Position = new Vector2f(position.X + x, position.Y + y + item.Margin.Top);
                        y += item.GetBounds().Height + item.Margin.Bottom;
                    }
                    break;
                case HorizontalAlignment.Right:
                    foreach (UIElement item in Items)
                    {
                        float x = (maxWidth - item.GetBounds().Width);
                        item.Position = new Vector2f(position.X + x, position.Y + y + item.Margin.Top);
                        y += item.GetBounds().Height + item.Margin.Bottom;
                    }
                    break;
            }
        }

        private void ToReconstructHorizon()
        {
            float maxHeight = GetMaxHeightItem();
            float x = 0;
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    foreach (UIElement item in Items)
                    {
                        item.Position = new Vector2f(position.X + x + item.Margin.Left, position.Y);
                        x += item.GetBounds().Width + item.Margin.Right;
                    }
                    break;
                case VerticalAlignment.Center:
                    foreach (UIElement item in Items)
                    {
                        float y = (maxHeight - item.GetBounds().Height) / 2;
                        item.Position = new Vector2f(position.X + x + item.Margin.Left, position.Y + y);
                        x += item.GetBounds().Width + item.Margin.Right;
                    }
                    break;
                case VerticalAlignment.Bottom:
                    foreach (UIElement item in Items)
                    {
                        float y = (maxHeight - item.GetBounds().Height);
                        item.Position = new Vector2f(position.X + x + item.Margin.Left, position.Y + y);
                        x += item.GetBounds().Width + item.Margin.Right;
                    }
                    break;
            }
        }

        private float GetMaxWidthItem()
        {
            float width = 0;
            foreach (UIElement item in Items)
            {
                float w = item.GetBounds().Width;
                width = width < w ? w : width;
            }
            return width;
        }

        private float GetMaxHeightItem()
        {
            float height = 0;
            foreach (UIElement item in Items)
            {
                float h = item.GetBounds().Height;
                height = height < h ? h : height;
            }
            return height;
        }
    }
}