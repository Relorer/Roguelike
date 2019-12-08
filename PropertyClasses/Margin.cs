namespace Roguelike.PropertyClasses
{
    public struct Margin
    {
        public float Top { get; set; }

        public float Bottom { get; set; }

        public float Left { get; set; }

        public float Right { get; set; }

        public Margin(float margin)
        {
            Top = margin;
            Bottom = margin;
            Left = margin;
            Right = margin;
        }

        public Margin(float left, float top, float right, float bottom)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            return "Top: " + Top + "; Bottom: " + Bottom + "; Left: " + Left + "; Right: " + Right;
        }
    }
}
