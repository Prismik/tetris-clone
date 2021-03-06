﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris.States
{
    abstract class GameState
    {
        public InputState InputState { get; private set; }
        public bool RequireUpdate { get; set; }
        public StateManager Manager { get; private set; }
        public GameState(StateManager manager)
        {
            Manager = manager;
            InputState = new InputState();
            RequireUpdate = true;
        }

        public virtual void LoadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            InputState.Update();
        }

        public abstract void Draw();
    }
}
