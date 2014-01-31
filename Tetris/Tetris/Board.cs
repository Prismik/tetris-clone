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
        InputState _input;
        Score _score;
        Level _level;
        Tetromino _tetromino;
        GhostPiece _ghost;
        Holder _holder;
        Preview _preview;
        SpriteBatch _spriteBatch;
        SpriteFont _font;
        Texture2D _texture;
        Texture2D _ghostTexture;
        int _timer;
        IAudioManager _audio;
        Vector2 _offset;
        PlayerIndex _index;
        ControlsConfig _controls;
        /// <summary>
        /// Initializes the board and it's component.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="font"></param>
        /// <param name="text"></param>
        public Board(SpriteBatch sb, SpriteFont font, Texture2D text, Texture2D ghost, Game game, 
            InputState input, Vector2 location, PlayerIndex pIndex, ControlsConfig controls)
        {
            _controls = controls;
            _index = pIndex;
            _offset = location;
            _input = input;
            _texture = text;
            _font = font;
            _ghostTexture = ghost;
            _holder = new Holder(_font, _offset);
            _score = new Score(_font, _offset);
            _level = new Level(_font, _offset);
            _preview = new Preview(_font, _buffer, _offset);
            _spriteBatch = sb;
            Grid = new Block[22, 10];
            NewTetromino();
            _audio = (IAudioManager)game.Services.GetService(typeof(IAudioManager));
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
            CreateGhostPiece();
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

        /// <summary>
        /// Moves the tetromino to the ghost piece location instantly.
        /// </summary>
        /// <returns>Returns true if the hard drop has succeeded.</returns>
        private bool HardMoveTetromino()
        {
            int movement = (int)(_ghost._tetromino.Blocks[0].Position.Y - _tetromino.Blocks[0].Position.Y);
            WipeTetromino(_tetromino); // Old position
            IEnumerator enum1 = _ghost._tetromino.Blocks.GetEnumerator();
            IEnumerator enum2 = _tetromino.Blocks.GetEnumerator();
            while ((enum1.MoveNext()) && (enum2.MoveNext()))
                ((Block)enum2.Current).Position = ((Block)enum1.Current).Position;
            
            ReplaceTetromino(_tetromino); // New position
            LockTetromino();
            _score.HandleHardDropEvent(movement);
            return true;
        }

        /// <summary>
        /// Evaluates if the player is dead.
        /// </summary>
        /// <returns>True if the player is dead.</returns>
        private bool EvaluateIfDead()
        {
            for (int i = 0; i != 2; i++)
                for (int j = 0; j != Grid.GetLength(1); j++)
                    if (_tetromino.PointInsideTetromino(new Vector2(j, i)))
                        return true;

            return false;
        }

        /// <summary>
        /// Lock the current tetromino.
        /// </summary>
        /// <returns></returns>
        private bool LockTetromino()
        {
            _score.Dead = EvaluateIfDead();

            int nbLines = ClearLines();

            NewTetromino(); // Adds a new tetromino
            _holder.Active = true; // Holder function are activated

            totalLines += nbLines;
            _score.HandleClearLinesEvent(nbLines, _level.CurrentLevel);
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
            _audio.PlaySound("button1");
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
            // Begins at 2 so that the tetromino can't be seen upon spawning.
            for (int i = 2; i != Grid.GetLength(0); i++)
                for (int j = 0; j != Grid.GetLength(1); j++)
                {
                    Block b = (Block)Grid.GetValue(i, j);
                    if (b != null)
                        b.Draw(_spriteBatch, _texture, (int)_offset.X + j * 16, (int)_offset.Y + 16 * i);
                    else
                        Draw("0", (int)_offset.X + j * 16, (int)_offset.Y +16 * i, Color.White);

                    foreach (Block block in _ghost._tetromino.Blocks)
                        if (block.Position.X == j && block.Position.Y == i)
                            block.Draw(_spriteBatch, _texture, (int)_offset.X + j * 16, (int)_offset.Y + 16 * i);
                }
        }

        private void Draw(string s, int x, int y, Color c)
        {
            Rectangle rect = new Rectangle(x, y, 16, 16);
            _spriteBatch.Draw(_ghostTexture, rect, null, c);
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

            if (_input.IsKeyUp(_das.key))
                _das.StopDASTimer();

            if (_das.DASEnabled)
                _das.IncrementDelayTimer(gameTime.ElapsedGameTime.Milliseconds, MoveTetromino, _score.HandleSoftDropEvent);

            PlayerIndex useless;
   
            if (_input.IsNewKeyPress(_controls.Right, _index, out useless))
            {
                MoveTetromino(Directions.Right);
                _das.StartDASTimer(Directions.Right, _controls.Right);
            }

            if (_input.IsNewKeyPress(_controls.Bottom, _index, out useless))
            {
                MoveTetromino(Directions.Bottom);
                _das.StartDASTimer(Directions.Bottom, _controls.Bottom);
            }

            if (_input.IsNewKeyPress(_controls.Left, _index, out useless))
            {
                MoveTetromino(Directions.Left);
                _das.StartDASTimer(Directions.Left, _controls.Left);
            }

            if (_input.IsNewKeyPress(_controls.Rotate, _index, out useless))
                RotateTetromino();

            if (_input.IsNewKeyPress(_controls.Hold, _index, out useless))
                HoldTetromino();

            if (_input.IsNewKeyPress(_controls.Drop, _index, out useless))
                HardMoveTetromino();
        }

        public void Draw()
        {
            if (!_score.Dead)
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
}
