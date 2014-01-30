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
        public LocalMultiplayer(SpriteBatch spriteBatch, SpriteFont font, Texture2D texture, Texture2D ghost, Game game)
            :base()
        {
            _spriteBatch = spriteBatch;
          
            _firstBoard = new Board(spriteBatch, font, texture, ghost, game, InputState, new Vector2(50, 0), PlayerIndex.One);
            _secondBoard = new Board(spriteBatch, font, texture, ghost, game, InputState, new Vector2(550, 0),PlayerIndex.Two);
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
