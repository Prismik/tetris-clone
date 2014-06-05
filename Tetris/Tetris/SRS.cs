using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Entities.Tetromino;
using Microsoft.Xna.Framework;
namespace Tetris
{
    /// <summary>
    /// Handles the calculation of wallkicks for tetromino rotations.
    /// </summary>
    class SRS
    {
        List<Vector2>[] _TLSTZoffsets = new List<Vector2>[4];
        List<Vector2>[] _Ioffsets = new List<Vector2>[4];
        public SRS()
        {
            ////////////////////
            // TLSTZ Tetrominoes
            // Init
            for (int i = 0; i != 4; i++)
                _TLSTZoffsets[i] = new List<Vector2>();

            // Origin
            for (int i = 0; i != 5; i++)
                _TLSTZoffsets[0].Add(new Vector2(0, 0));

            // Right
            _TLSTZoffsets[1].Add(new Vector2(0, 0));
            _TLSTZoffsets[1].Add(new Vector2(1, 0));
            _TLSTZoffsets[1].Add(new Vector2(1, -1));
            _TLSTZoffsets[1].Add(new Vector2(0, 2));
            _TLSTZoffsets[1].Add(new Vector2(1, 2));

            // Counter Clockwise 
            for (int i = 0; i != 5; i++)
                _TLSTZoffsets[2].Add(new Vector2(0, 0));

            // Left
            _TLSTZoffsets[3].Add(new Vector2(0, 0));
            _TLSTZoffsets[3].Add(new Vector2(-1, 0));
            _TLSTZoffsets[3].Add(new Vector2(-1, -1));
            _TLSTZoffsets[3].Add(new Vector2(0, 2));
            _TLSTZoffsets[3].Add(new Vector2(-1, 2));
            
            ////////////////
            // I Tetromino
            // Init
            for (int i = 0; i != 4; i++)
                _Ioffsets[i] = new List<Vector2>();

            // Origin
            _Ioffsets[0].Add(new Vector2(0, 0));
            _Ioffsets[0].Add(new Vector2(-1, 0));
            _Ioffsets[0].Add(new Vector2(2, 0));
            _Ioffsets[0].Add(new Vector2(-1, 0));
            _Ioffsets[0].Add(new Vector2(2, 0));

            // Right
            _Ioffsets[1].Add(new Vector2(-1, 0));
            _Ioffsets[1].Add(new Vector2(0, 0));
            _Ioffsets[1].Add(new Vector2(0, 0));
            _Ioffsets[1].Add(new Vector2(0, 1));
            _Ioffsets[1].Add(new Vector2(0, -2));


            _Ioffsets[2].Add(new Vector2(-1, 1));
            _Ioffsets[2].Add(new Vector2(1, 1));
            _Ioffsets[2].Add(new Vector2(-2, 1));
            _Ioffsets[2].Add(new Vector2(1, 0));
            _Ioffsets[2].Add(new Vector2(-2, 0));

            _Ioffsets[3].Add(new Vector2(0, 1));
            _Ioffsets[3].Add(new Vector2(0, 1));
            _Ioffsets[3].Add(new Vector2(0, 1));
            _Ioffsets[3].Add(new Vector2(0, -1));
            _Ioffsets[3].Add(new Vector2(0, 2));
        }

        public List<Vector2> WallkickValues(Tetromino t)
        {
            List<Vector2> wallkicks = new List<Vector2>();
            switch (t.Type)
            {
                case TetrominoTypes.J:
                case TetrominoTypes.L:
                case TetrominoTypes.S:
                case TetrominoTypes.T:
                case TetrominoTypes.Z:
                    wallkicks = GetWallKick(_TLSTZoffsets, t);
                    break;
                case TetrominoTypes.I:
                    wallkicks = GetWallKick(_Ioffsets, t);
                    break;
                default:
                    break;
            }

            return wallkicks;
        }

        private List<Vector2> GetWallKick(List<Vector2>[] offsets, Tetromino t)
        {
            List<Vector2> wallkicks = new List<Vector2>();
            for (int i = 0; i != offsets[0].Count; i++)
            {
                int x = (int)(offsets[(int)t.GetLastRotation()][i].X - offsets[(int)t.CurrentRotation][i].X);
                int y = (int)(offsets[(int)t.GetLastRotation()][i].Y - offsets[(int)t.CurrentRotation][i].Y);
                wallkicks.Add(new Vector2(x, y));
            }

            RotationTypes ty = t.GetLastRotation();
            return wallkicks;
        }
    }
}
