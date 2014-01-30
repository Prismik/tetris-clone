using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris.States
{
    abstract class GameState
    {
        public InputState InputState { get; private set; }
        public GameState()
        {
            InputState = new InputState();
        }

        public virtual void Update(GameTime gameTime)
        {
            InputState.Update();
        }

        public abstract void Draw();
    }
}
