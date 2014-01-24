using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Tetris.Entities;

namespace Tetris.Entities.Tetromino
{
    internal class TetrominoFactory
    {
        public static Block[] BuildI()
        {
            Color c = Color.Cyan;
            return new Block[] { new Block(1, 0, c), new Block(0, 0, c), new Block(2, 0, c), new Block(3, 0, c) };
        }

        public static Block[] BuildO()
        {
            Color c = Color.Yellow;
            return new Block[] { new Block(0, 0, c), new Block(0, 1, c), new Block(1, 0, c), new Block(1, 1, c) };
        }

        public static Block[] BuildT()
        {
            Color c = Color.Purple;
            return new Block[] { new Block(1, 1, c), new Block(1, 0, c), new Block(0, 1, c), new Block(2, 1, c) };
        }

        public static Block[] BuildJ()
        {
            Color c = Color.Blue;
            return new Block[] { new Block(1, 1, c), new Block(0, 0, c), new Block(0, 1, c), new Block(2, 1, c) };
        }

        public static Block[] BuildL()
        {
            Color c = Color.Orange;
            return new Block[] { new Block(1, 1, c), new Block(2, 0, c), new Block(0, 1, c), new Block(2, 1, c) };
        }

        public static Block[] BuildS()
        {
            Color c = Color.Green;
            return new Block[] { new Block(1, 0, c), new Block(2, 0, c), new Block(0, 1, c), new Block(1, 1, c) };
        }

        public static Block[] BuildZ()
        {
            Color c = Color.Red;
            return new Block[] { new Block(1, 0, c), new Block(0, 0, c), new Block(1, 1, c), new Block(2, 1, c) };
        }
    }
}
