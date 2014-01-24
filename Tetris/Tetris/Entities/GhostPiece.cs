using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Entities
{
    class GhostPiece
    {
        
        public Tetromino.Tetromino _tetromino;
        public Point Position { get; set; }
        public GhostPiece(Tetromino.Tetromino t)
        {
            _tetromino = new Tetromino.Tetromino(t);
            ChangeColor();
        }

        private void ChangeColor()
        {
            foreach (Block b in _tetromino.Blocks)
                b.Color = Color.White;
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            
            _tetromino.Draw(sb, texture, (int)_tetromino.Blocks[0].Position.X, (int)_tetromino.Blocks[0].Position.Y);

        }
    }
}
