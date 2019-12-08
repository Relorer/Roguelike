using Roguelike.PropertyClasses;
using SFML.Graphics;
using SFML.System;

namespace Roguelike.UIElements
{
    public enum Orientation { Horizontal, Vertical }

    public enum VerticalAlignment { Top, Center, Bottom }

    public enum HorizontalAlignment { Left, Center, Right }

    public enum State { Free, Hovered, Clamped, Clicked }

    public interface UIElement : Drawable
    {
        State State { get; set; }

        Vector2f Position { get; set; }

        Margin Margin { get; set; }

        FloatRect GetBounds();
    }
}
