using SFML.System;

namespace Roguelike
{
    static class StaticClock
    {
        static public Clock Clock { get; private set; }

        static public Time ElapsedTime {
            get {
                return Clock.ElapsedTime;
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