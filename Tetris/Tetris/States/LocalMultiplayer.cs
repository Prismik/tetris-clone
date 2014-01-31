using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris.States
{
    class LocalMultiplayer: GameState
    {
        Board _firstBoard, _secondBoard;
        SpriteBatch _spriteBatch;
        public LocalMultiplayer(SpriteBatch spriteBatch, SpriteFont font, Texture2D texture, Texture2D ghost, Game game, StateManager manager)
            :base(manager)
        {
            _spriteBatch = spriteBatch;

            ControlsConfig left = InitLeftControl();
            ControlsConfig right = InitRightControl();
            _firstBoard = new Board(spriteBatch, font, texture, ghost, game, InputState, new Vector2(50, 0), PlayerIndex.One, left);
            _secondBoard = new Board(spriteBatch, font, texture, ghost, game, InputState, new Vector2(550, 0),PlayerIndex.Two, right);
        }

        private ControlsConfig InitLeftControl()
        {
            ControlsConfig config = new ControlsConfig();
            config.Left = Microsoft.Xna.Framework.Input.Keys.Left;
            config.Bottom = Microsoft.Xna.Framework.Input.Keys.Down;
            config.Right = Microsoft.Xna.Framework.Input.Keys.Right;
            config.Rotate = Microsoft.Xna.Framework.Input.Keys.Space;
            config.Hold = Microsoft.Xna.Framework.Input.Keys.LeftShift;
            config.Drop = Microsoft.Xna.Framework.Input.Keys.D;

            return config;
        }

        private ControlsConfig InitRightControl()
        {
            ControlsConfig config = new ControlsConfig();
            config.Left = Microsoft.Xna.Framework.Input.Keys.Left;
            config.Bottom = Microsoft.Xna.Framework.Input.Keys.Down;
            config.Right = Microsoft.Xna.Framework.Input.Keys.Right;
            config.Rotate = Microsoft.Xna.Framework.Input.Keys.Space;
            config.Hold = Microsoft.Xna.Framework.Input.Keys.LeftShift;
            config.Drop = Microsoft.Xna.Framework.Input.Keys.D;

            return config;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _firstBoard.Update(gameTime);
            _secondBoard.Update(gameTime);
        }

        public override void Draw()
        {
            _firstBoard.Draw();
            _secondBoard.Draw();
        }
    }
}
