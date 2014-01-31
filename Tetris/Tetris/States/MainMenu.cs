using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.States
{
    class MainMenu : GameState
    {
        SpriteBatch _spriteBatch;   
        public MainMenu(StateManager manager)
            :base(manager)
        {

        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            PlayerIndex useless;
            if (InputState.IsNewKeyPress(Microsoft.Xna.Framework.Input.Keys.S, null, out useless))
            {
                Manager.AddState(new SinglePlayer(Manager.SpriteBatch, Manager));
                Manager.RemoveState(this);
            }
        }

        public override void Draw()
        {
          
        }
    }
}
