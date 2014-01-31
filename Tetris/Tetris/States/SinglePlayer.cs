using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris.States
{
    class SinglePlayer: GameState
    {
        Board _board;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D _texture;
        Texture2D _ghost;
        public SinglePlayer(SpriteBatch spriteBatch, StateManager manager)
            : base(manager)
        {
            _spriteBatch = spriteBatch;
            LoadContent();
            _board = new Board(spriteBatch, _font, _texture, _ghost, Manager.Game, InputState, new Vector2(50, 0), PlayerIndex.One, InitControls());
        }

        private ControlsConfig InitControls()
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

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("gamefont");
            _texture = Manager.Game.Content.Load<Texture2D>("block");
            _ghost = Manager.Game.Content.Load<Texture2D>("ghost");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _board.Update(gameTime);
        }

        public override void Draw()
        {
            _board.Draw();
        }
    }
}
