using SFML.System;

namespace Roguelike
{
    static class StaticClock
    {
        static public Clock Clock { get; private set; }

        static public long ElapsedMilliseconds {
            get {
                return Clock.ElapsedTime.AsMilliseconds();
            }
        }

        static StaticClock()
        {
            Clock = new Clock();
        }

        static public void Restart()
        {
            Clock.Restart();
        }
    }
}