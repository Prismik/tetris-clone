using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Entities.Tetromino;
using Microsoft.Xna.Framework;
namespace Tetris
{
    /// <summary>
    /// Handles wallkick for tetrominoes.
    /// </summary>
    class SRS
    {
        List<Vector2>[] _TLSTZoffsets = new List<Vector2>[4];
        List<Vector2>[] _Ioffsets = new List<Vector2>[4];
        List<Vector2>[] _Ooffsets = new List<Vector2>[4];
        public SRS()
        {
            // Origin
            for (int i = 0; i != 4; i++)
                _TLSTZoffsets[0].Add(new Vector2(0, 0));

            // Right
            _TLSTZoffsets[1].Add(new Vector2(0, 0));
            _TLSTZoffsets[1].Add(new Vector2(1, 0));
            _TLSTZoffsets[1].Add(new Vector2(1, -1));
            _TLSTZoffsets[1].Add(new Vector2(0, 2));
            _TLSTZoffsets[1].Add(new Vector2(1, 2));

            // Counter Clockwise
            for (int i = 0; i != 4; i++)
                _TLSTZoffsets[2].Add(new Vector2(0, 0)); 

            // Left
            _TLSTZoffsets[3].Add(new Vector2(0, 0));
            _TLSTZoffsets[3].Add(new Vector2(-1, 0));
            _TLSTZoffsets[3].Add(new Vector2(-1, -1));
            _TLSTZoffsets[3].Add(new Vector2(0, 2));
            _TLSTZoffsets[3].Add(new Vector2(-1, 2));
        }

        public void Wallkick(Tetromino t)
        {
            switch (t.Type)
            {
                case TetrominoTypes.J:
                case TetrominoTypes.L:
                case TetrominoTypes.S:
                case TetrominoTypes.T:
                case TetrominoTypes.Z:
                    
                    break;
                case TetrominoTypes.I:

                    break;
                case TetrominoTypes.O:
                    
                    break;
                default:
                    break;
            }
        }
    }
}
