using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris.States
{
    abstract class GameState
    {
        public InputState State { get; private set; }
        public GameState()
        {
            State = new InputState();
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw();
    }
}
