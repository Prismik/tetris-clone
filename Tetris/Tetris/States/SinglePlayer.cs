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
        public SinglePlayer(SpriteBatch spriteBatch, SpriteFont font, Texture2D texture, Texture2D ghost, Game game)
            : base()
        {
            _spriteBatch = spriteBatch;
            _board = new Board(spriteBatch, font, texture, ghost, game, InputState, new Vector2(50, 0), PlayerIndex.One);
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
