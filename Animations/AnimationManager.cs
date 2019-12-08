using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Roguelike.Animations
{
    class AnimationManager
    {
        private Dictionary<string, Animation> Animations;

        private Animation CurrentAnimation;
        public string Current {
            set {
                if ((CurrentAnimation == null || !CurrentAnimation.Stringent) && CurrentAnimation != Animations[value])
                {
                    CurrentAnimation?.Restart();
                    CurrentAnimation = Animations[value];
                }
            }
        }

        public AnimationManager()
        {
            Animations = new Dictionary<string, Animation>();
        }

        public Sprite GetSprite(Vector2f position)
        {
            Sprite sprite = CurrentAnimation.GetSprite();
            sprite.Position = position;
            return sprite;
        }

        public void Add(Animation animation, string name)
        {
            Animations.Add(name, animation);
            animation.Ended += Animation_Ended;
        }

        private void Animation_Ended(object sender, EventArgsClases.AnimationEndedEventArgs e)
        {
            CurrentAnimation = null;
        }
    }
}