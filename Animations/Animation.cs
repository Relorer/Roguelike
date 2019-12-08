using Roguelike.EventArgsClases;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Roguelike.Animations
{
    class Animation
    {
        public event EventHandler<AnimationEndedEventArgs> Ended;

        public bool Stringent { get; private set; }

        private bool Repetitive;

        private Sprite Sprite;

        private List<IntRect> Frames;

        private readonly long FrameTime;

        private long ElapsedTime;

        public Animation(Texture texture, Vector2i start, Vector2i size, int count, long time, bool repetitive = true, bool stringent = false)
        {
            Sprite = new Sprite(texture);
            Frames = new List<IntRect>();
            for (int i = 0; i < count; i++)
            {
                Frames.Add(new IntRect(start.X + i * size.X, start.Y, size.X, size.Y));
            }
            FrameTime = time / count;
            Repetitive = repetitive;
            Stringent = stringent;
        }

        public Sprite GetSprite()
        {
            ElapsedTime += StaticClock.ElapsedMilliseconds;
            if ((int)(ElapsedTime / FrameTime) >= Frames.Count) Restart();
            Sprite.TextureRect = Frames[(int)(ElapsedTime / FrameTime)];
            return Sprite;
        }

        public void Restart()
        {
            ElapsedTime = 0;
            if (!Repetitive) Ended.Invoke(this, new AnimationEndedEventArgs());
        }
    }
}