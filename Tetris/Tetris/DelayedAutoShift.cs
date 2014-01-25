using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    public class DelayedAutoShift
    {
        int DASTimer = -1;
        public bool TimerStopped { get { return DASTimer == -1; } }
        public const int DAS_DELAY = 50;
        public int DASDelayTimer { get; set; }
        public bool DASEnabled { get; private set; }
        public Vector2 DASDirection { get; private set; }
        public Keys key;
        
        public DelayedAutoShift()
        {
            DASDirection = Vector2.Zero;
            DASEnabled = false;
            DASDelayTimer = 0;
        }

        public void StartDASTimer(Vector2 direction, Keys keyPress)
        {
            DASDirection = direction;
            DASTimer = 0;
            DASDelayTimer = 0;
            DASEnabled = false;
            key = keyPress;
        }

        public void StopDASTimer()
        {
            DASDirection = Vector2.Zero;
            DASTimer = -1;
            DASDelayTimer = 0;
            DASEnabled = false;
            key = Keys.None;
        }

        public void IncrementTimer(int milliseconds)
        {
            DASTimer += milliseconds;
            if (DASTimer > 500)
                DASEnabled = true;
        }

        public void IncrementDelayTimer(int milliseconds, Func<Vector2, bool> action)
        {
            DASDelayTimer += milliseconds;
            if (DASDelayTimer > DelayedAutoShift.DAS_DELAY)
            {
                DASDelayTimer = 0;
                action(DASDirection);
            }
        }
    }
}
