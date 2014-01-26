using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Entities
{
    /// <summary>
    /// A block part of a tetromino.
    /// </summary>
    public class Block
    {
        public int Value { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Block(Block b)
        {
            Value = b.Value;
            Position = b.Position;
            Color = b.Color;
        }

        public Block(Vector2 pos)
        {
            Value = 1;
            Position = pos;
        }

        public Block(float x, float y)
        {
            Position = new Vector2(x, y);
            Value = 1;
        }

        public Block(float x, float y, Color color)
        {
            Position = new Vector2(x, y);
            Value = 1;
            Color = color;
        }

        /// <summary>
        /// Moves the block by a flat ammount.
        /// </summary>
        /// <param name="p">The vector representing the flat ammount by which the block is moved.</param>
        public void Translate(Vector2 p)
        {
            Position = Position + p;
        }

        /// <summary>
        /// Draws the block.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(SpriteBatch sb, Texture2D texture, int x, int y)
        {
            Rectangle rect = new Rectangle(x, y, 16, 16);
            sb.Draw(texture, rect, null, Color);
        }
    }
}
