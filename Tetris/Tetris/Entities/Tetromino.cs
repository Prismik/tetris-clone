using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Entities.Tetromino
{
    public enum TetrominoTypes { I, O, T, J, L, S, Z }

    /// <summary>
    /// Represents a tetromino piece that can be controlled by the player. A tetromino is used
    /// in order to complete rows and gain points. It is composed of 4 blocks.
    /// </summary>
    public class Tetromino
    {
        public TetrominoTypes Type { get; protected internal set; }
        public Block[] Blocks { get; protected internal set; }
        private Color _color;
        private Tetromino()
        {
            Blocks = new Block[4];
        }

        public Tetromino(TetrominoTypes t)
        {
            Blocks = new Block[4];
            Type = t;
            switch (Type)
            {
                case TetrominoTypes.I: Blocks = TetrominoFactory.BuildI(); break;
                case TetrominoTypes.O: Blocks = TetrominoFactory.BuildO(); break;
                case TetrominoTypes.T: Blocks = TetrominoFactory.BuildT(); break;
                case TetrominoTypes.J: Blocks = TetrominoFactory.BuildJ(); break;
                case TetrominoTypes.L: Blocks = TetrominoFactory.BuildL(); break;
                case TetrominoTypes.S: Blocks = TetrominoFactory.BuildS(); break;
                case TetrominoTypes.Z: Blocks = TetrominoFactory.BuildZ(); break;
                default: throw (new InvalidOperationException());
            }
            _color = Blocks[0].Color;
        }

        public Tetromino(Tetromino tetromino)
        {
            Blocks = new Block[4];
            for (int i = 0; i != Blocks.Length; i++)
                Blocks[i] = new Block(tetromino.Blocks[i]);

            Type = tetromino.Type;
            _color = tetromino.Blocks[0].Color;
        }

        /// <summary>
        /// Moves the tetromino by a flat ammount.
        /// </summary>
        /// <param name="p">The vector representing the flat ammount by which the tetromino is moved.</param>
        public void Translate(Vector2 p)
        {
            for (int i = 0; i != Blocks.Length; i++)
                Blocks[i].Translate(p);
        }

        /// <summary>
        /// Rotates the tetromino around an axis. This axis is presently the 2nd block of the tetromino.
        /// </summary>
        public void Rotate()
        {
            if (Type.Equals(TetrominoTypes.O))
                return;
            
            // First vector in the block is always center of rotation
            Vector2 middle = Blocks[0].Position;

            List<Vector2> rel = new List<Vector2>();
            // Get each point relative to the middle
            foreach (Block b in Blocks)
                rel.Add(new Vector2(b.Position.X - middle.X, b.Position.Y - middle.Y));

            List<Block> shape = new List<Block>();
            // Build the new rotated shape by rotating around the middle
            foreach (Vector2 p in rel)
                shape.Add(new Block(middle.X - p.Y, middle.Y + p.X, _color));

            // Convert List to Array
            Blocks = shape.ToArray();
        }

        /// <summary>
        /// Determines wether a point is inside the tetromino or not.
        /// </summary>
        /// <param name="p">The point to be tested.</param>
        /// <returns>True if the point is inside the tetromino; otherwize false.</returns>
        public bool PointInsideTetromino(Vector2 p)
        {
            foreach (Block b in Blocks)
                if (b.Position == p)
                    return true;

            return false;
        }

        /// <summary>
        /// Returns a fresh new tetromino of the same type.
        /// </summary>
        /// <returns>The new tetromino.</returns>
        public Tetromino Normalize()
        {
            return new Tetromino(this.Type);
        }
        
        /// <summary>
        /// Draws the tetromino.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(SpriteBatch sb, Texture2D texture, int x, int y)
        {
            foreach (Block b in Blocks)
                b.Draw(sb, texture, (int)b.Position.X * 16 + x, (int)b.Position.Y * 16 + y);
        }
    }
}
