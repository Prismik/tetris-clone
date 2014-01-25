using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Tetris.Entities.Tetromino;
using Tetris.Entities;
using System.Collections;

namespace Tetris
{
    /// <summary>
    /// The board contains the tetrominos and is the main playing area that the player uses.
    /// </summary>
    class Board
    {
        int totalLines;
        public Block[,] Grid { get; set; }
        DelayedAutoShift _das = new DelayedAutoShift();
        TetrominoBuffer _buffer = new TetrominoBuffer();
        InputState _input = new InputState();
        Score _score;
        Level _level;
        Tetromino _tetromino;
        GhostPiece _ghost;
        Holder _holder;
        Preview _preview;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D _texture;
        int _timer;

        /// <summary>
        /// Initializes the board and it's component.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="font"></param>
        /// <param name="text"></param>
        public Board(SpriteBatch sb, SpriteFont font, Texture2D text)
        {
            _texture = text;
            _font = font;
            _holder = new Holder(_font);
            _score = new Score(_font);
            _level = new Level(_font);
            _preview = new Preview(_font, _buffer);
            _spriteBatch = sb;
            Grid = new Block[22, 10];
            NewTetromino();
        }

        /// <summary>
        /// Create a new tetromino to be controlled by the player.
        /// </summary>
        public void NewTetromino()
        {
            _preview.Next();
            _tetromino = new Tetromino((TetrominoTypes)_buffer.GetNext());
            CreateGhostPiece();
        }

        /// <summary>
        /// Remove the tetromino from the board.
        /// </summary>
        /// <param name="t"></param>
        private void WipeTetromino(Tetromino t)
        {
            foreach (Block p in t.Blocks)
                Grid[(int)p.Position.Y, (int)p.Position.X] = null;
        }

        /// <summary>
        /// Puts the tetrimino at a new location.
        /// </summary>
        /// <param name="t"></param>
        private void ReplaceTetromino(Tetromino t)
        {
            foreach (Block p in t.Blocks)
                Grid[(int)p.Position.Y, (int)p.Position.X] = p;
        }

        /// <summary>
        /// Creates and puts a new tetromino under the player's control.
        /// </summary>
        /// <param name="t"></param>
        private void NewTetromino(TetrominoTypes t)
        {
            _tetromino = new Tetromino(t);
        }

        /// <summary>
        /// Handle the move input.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool MoveTetromino(Vector2 direction)
        {
            // Validate if the tetromino can be moved in such direction ...
            foreach (Block p in _tetromino.Blocks)
            {
                if (PointOutsideBoardX(p.Position + direction))
                    return false;

                if (PointOutsideBoardY(p.Position + direction))
                {
                    LockTetromino();
                    return false;
                }

                if (direction.X == 0 && !_tetromino.PointInsideTetromino(p.Position + direction) &&
                    PointAlreadyOccupied(p.Position + direction))
                {
                    LockTetromino();
                    return false;
                }

                if (!_tetromino.PointInsideTetromino(p.Position + direction) &&
                    PointAlreadyOccupied(p.Position + direction))
                    return false;
            }

            WipeTetromino(_tetromino); // Old position
            _tetromino.Translate(direction); // Move
            ReplaceTetromino(_tetromino); // New position

            if (direction == Directions.Left || direction == Directions.Right)
                CreateGhostPiece();

            return true; // Success
        }

        private bool HardMoveTetromino()
        {
            WipeTetromino(_tetromino); // Old position
            IEnumerator enum1 = _ghost._tetromino.Blocks.GetEnumerator();
            IEnumerator enum2 = _tetromino.Blocks.GetEnumerator();
            while ((enum1.MoveNext()) && (enum2.MoveNext()))
                ((Block)enum2.Current).Position = ((Block)enum1.Current).Position;
            
            ReplaceTetromino(_tetromino); // New position
            LockTetromino();
            return true;
        }

        /// <summary>
        /// Lock the current tetromino.
        /// </summary>
        /// <returns></returns>
        private bool LockTetromino()
        {
            NewTetromino(); // Adds a new tetromino
            _holder.Active = true; // Holder function are activated
            int nbLines = ClearLines();
            totalLines += nbLines;
            _score.HandleScoreEvent(nbLines, _level.CurrentLevel);
            if (totalLines > 10)
            {
                totalLines %= 10;
                _level.LevelUp();
            }
            return true;
        }

        /// <summary>
        /// Handle the rotation input.
        /// </summary>
        /// <returns></returns>
        private bool RotateTetromino()
        {
            // Validate if tetromino can be rotated
            Tetromino copy = new Tetromino(_tetromino);
            copy.Rotate();
            foreach (Block p in copy.Blocks)
            {
                if (PointOutsideBoardX(p.Position))
                    return false;

                if (PointOutsideBoardY(p.Position))
                    //LockTetromino();
                    return false;

                if (!_tetromino.PointInsideTetromino(p.Position) &&
                    PointAlreadyOccupied(p.Position))
                    //LockTetromino();
                    return false;
            }

            WipeTetromino(_tetromino);  // Old position
            _tetromino.Rotate(); // Rotate
            ReplaceTetromino(_tetromino); // New position

            CreateGhostPiece();
            return true;
        }

        /// <summary>
        /// Removes the tetromino from the board and keep it in the holder.
        /// </summary>
        /// <returns>True if the hold action succeeded; otherwize, false</returns>
        public bool HoldTetromino()
        {
            if (_holder.Active)
            {
                Tetromino pushTop = _holder.Hold(_tetromino);
                WipeTetromino(_tetromino);
                if (pushTop != null)
                    NewTetromino(pushTop.Type);
                else
                    NewTetromino();

                return true;
            }
            else
                return false;
        }

        private void CalculateGhostPiecePosition(Tetromino ghost)
        {
            while (true)
            {
                foreach (Block b in ghost.Blocks)
                    if (!ghost.PointInsideTetromino(b.Position + Directions.Bottom))
                        if(PointOutsideBoardY(b.Position + Directions.Bottom) ||
                        PointAlreadyOccupied(b.Position + Directions.Bottom))
                            return;
                    
                ghost.Translate(Directions.Bottom);
            }
        }

        public void CreateGhostPiece()
        {
            Tetromino ghost = new Tetromino(_tetromino);
            CalculateGhostPiecePosition(ghost);
            _ghost = new GhostPiece(ghost);
        }

        /// <summary>
        /// Clear a line of blocks.
        /// </summary>
        /// <param name="line">the Y index of the line to clear.</param>
        private void ClearLine(int line)
        {
            for (int i = 0; i != Grid.GetLength(1); i++)
                Grid[line, i] = null;

            for (int i = line - 1; i != 0; i--)
                for (int j = 0; j != Grid.GetLength(1); j++)
                    Grid[i + 1, j] = Grid[i, j];
        }

        /// <summary>
        /// Clear all line that needs to be cleared.
        /// </summary>
        public int ClearLines()
        {
            Queue<int> linesToClear = new Queue<int>();
            for (int i = 0; i != Grid.GetLength(0); i++)
                if (!FoundEmpty(i))
                    linesToClear.Enqueue(i);

            int nbLines = linesToClear.Count;
            while (linesToClear.Count != 0)
                ClearLine(linesToClear.Dequeue());

            return nbLines;
        }

        private bool FoundEmpty(int i)
        {
            for (int j = 0; j != Grid.GetLength(1); j++)
                if (Grid[i, j] == null)
                    return true;

            return false;
        }

        /// <summary>
        /// Determines if a point is outside the vertical boundaries of the board.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool PointOutsideBoardY(Vector2 p)
        {
            return (p.Y < 0 || p.Y > Grid.GetLength(0)-1);
        }

        /// <summary>
        /// Determines if a point is outside the horizontal boundaries of the board.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool PointOutsideBoardX(Vector2 p)
        {
            return (p.X < 0 || p.X > Grid.GetLength(1)-1);
        }

        /// <summary>
        /// Determines if a point is occupied in the board.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool PointAlreadyOccupied(Vector2 p)
        {
            return Grid[(int)p.Y, (int)p.X] != null;
        }

        /// <summary>
        /// Print the board to the screen.
        /// </summary>
        private void PrintBoard()
        {
            for (int i = 0; i != Grid.GetLength(0); i++)
                for (int j = 0; j != Grid.GetLength(1); j++)
                {
                    Block b = (Block)Grid.GetValue(i, j);
                    if (b != null)
                        b.Draw(_spriteBatch, _texture, 50 + j * 16, 16 * i);
                    else
                        Draw("0", 50 + j * 16, 16 * i, Color.DarkGray);

                    foreach (Block block in _ghost._tetromino.Blocks)
                        if (block.Position.X == j && block.Position.Y == i)
                            block.Draw(_spriteBatch, _texture, 50 + j * 16, 16 * i);
                }
        }

        private void Draw(string s, int x, int y, Color c)
        {
            Rectangle rect = new Rectangle(x, y, 12, 12);
            _spriteBatch.Draw(_texture, rect, null, c);
        }

        public void Update(GameTime gameTime)
        {
            _timer += gameTime.ElapsedGameTime.Milliseconds;
            if (_timer >= _level.CurrentDelay)
            {
                MoveTetromino(Directions.Bottom);
                _timer = 0;
            }

            if (!_das.TimerStopped)
                _das.IncrementTimer(gameTime.ElapsedGameTime.Milliseconds);

            _input.Update(); // Update input
            PlayerIndex useless;

            if (_input.IsKeyUp(_das.key))
                _das.StopDASTimer();

            if (_das.DASEnabled)
                _das.IncrementDelayTimer(gameTime.ElapsedGameTime.Milliseconds, MoveTetromino);

            if (_input.IsNewKeyPress(Keys.D, null, out useless))
            {
                MoveTetromino(Directions.Right);
                _das.StartDASTimer(Directions.Right, Keys.D);
            }
       
            if (_input.IsNewKeyPress(Keys.S, null, out useless))
            {
                MoveTetromino(Directions.Bottom);
                _das.StartDASTimer(Directions.Bottom, Keys.S);
            }

            if (_input.IsNewKeyPress(Keys.A, null, out useless))
            {
                MoveTetromino(Directions.Left);
                _das.StartDASTimer(Directions.Left, Keys.A);
            }

            if (_input.IsNewKeyPress(Keys.Space, null, out useless))
                RotateTetromino();

            if (_input.IsNewKeyPress(Keys.LeftShift, null, out useless))
                HoldTetromino();

            if (_input.IsNewKeyPress(Keys.F, null, out useless))
                HardMoveTetromino();
        }

        public void Draw()
        {
            _spriteBatch.Begin();
            PrintBoard();
            _holder.Draw(_spriteBatch, _texture);
            _preview.Draw(_spriteBatch, _texture);
            _score.Draw(_spriteBatch);
            _level.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
