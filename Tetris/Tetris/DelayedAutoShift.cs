using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tetris
{
    /// <summary>
    /// Delayed movement behaviour upon holding a directional key pressed.
    /// </summary>
    public class DelayedAutoShift
    {
        int DASTimer = -1;
        public bool TimerStopped { get { return DASTimer == -1; } }

        /// <summary>
        /// The amount of time, in ms, required to enable de autoshift behaviour.
        /// </summary>
        public const int DAS_DELAY = 50;
        public int DASDelayTimer { get; set; }

        /// <summary>
        /// Determines if the autoshift behaviour is currently enabled or not.
        /// </summary>
        public bool DASEnabled { get; private set; }

        /// <summary>
        /// The direction related to the key pressed.
        /// </summary>
        public Vector2 DASDirection { get; private set; }

        /// <summary>
        /// The key pressed for which we handle the autoshift.
        /// </summary>
        public Keys key;
        
        public DelayedAutoShift()
        {
            DASDirection = Vector2.Zero;
            DASEnabled = false;
            DASDelayTimer = 0;
        }

        /// <summary>
        /// Starts the autoshift timer for a given direction.
        /// </summary>
        /// <param name="direction">The direction for which to start the timer.</param>
        /// <param name="keyPress">The key press bound to the direction.</param>
        public void StartDASTimer(Vector2 direction, Keys keyPress)
        {
            DASDirection = direction;
            DASTimer = 0;
            DASDelayTimer = 0;
            DASEnabled = false;
            key = keyPress;
        }

        /// <summary>
        /// Stops the autoshift timer.
        /// </summary>
        public void StopDASTimer()
        {
            DASDirection = Vector2.Zero;
            DASTimer = -1;
            DASDelayTimer = 0;
            DASEnabled = false;
            key = Keys.None;
        }

        /// <summary>
        /// Increments the current autoshift timer.
        /// </summary>
        /// <param name="milliseconds">The increment amount in milliseconds.</param>
        public void IncrementTimer(int milliseconds)
        {
            DASTimer += milliseconds;
            if (DASTimer > 150)
                DASEnabled = true;
        }

        /// <summary>
        /// Increments the autoshift delay timer to determines if the autoshift should be handling further
        /// tetromino move action calls in the current direction, along with the task of handling the score action call.
        /// </summary>
        /// <param name="milliseconds">The timer increment in milliseconds.</param>
        /// <param name="action">The action of moving a tetromino in a given direction.</param>
        /// <param name="scoreAction">The action of updating the score in case a row is cleared.</param>
        public void IncrementDelayTimer(int milliseconds, Func<Vector2, bool> action, Action scoreAction)
        {
            DASDelayTimer += milliseconds;
            if (DASDelayTimer > DAS_DELAY)
            {
                DASDelayTimer = 0;
                action(DASDirection);
                if (Directions.Bottom == DASDirection)
                    scoreAction();
            }
        }
    }
}
