using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class Score
    {
        public int CurrentScore { get; private set; }
        public bool Dead { get; set; }
        SpriteFont _font;
        Vector2 _offset;
        public Score(SpriteFont font, Vector2 location)
        {
            _offset = location;
            CurrentScore = 0;
            Dead = false;
            _font = font;
        }

        /// <summary>
        /// Calculates the score for clearing a single line.
        /// </summary>
        /// <param name="level">The current level.</param>
        /// <returns>The ammount of points added to the current score.</returns>
        private int SingleScore(int level)
        {
            return level * 100;
        }

        /// <summary>
        /// Calculates the score for clearing a single line.
        /// </summary>
        /// <param name="level">The current level.</param>
        /// <returns>The ammount of points added to the current score.</returns>
        private int DoubleScore(int level)
        {
            return level * 300;
        }

        /// <summary>
        /// Calculates the score for clearing a two lines.
        /// </summary>
        /// <param name="level">The current level.</param>
        /// <returns>The ammount of points added to the current score.</returns>
        private int TripleScore(int level)
        {
            return level * 500;
        }

        /// <summary>
        /// Calculates the score for clearing three lines.
        /// </summary>
        /// <param name="level">The current level.</param>
        /// <returns>The ammount of points added to the current score.</returns>
        private int TetrisScore(int level)
        {
            return level * 800;
        }

        /// <summary>
        /// Calculates the score for clearing a four lines.
        /// </summary>
        /// <param name="nbLines">The ammount of lines that was cleared.</param>
        /// <param name="level">The current level.</param>
        /// <returns>The ammount of points added to the current score.</returns>
        public void HandleClearLinesEvent(int nbLines, int level)
        {
            switch(nbLines)
            {
                case 1: CurrentScore += SingleScore(level); break;
                case 2: CurrentScore += DoubleScore(level); break;
                case 3: CurrentScore += TripleScore(level); break;
                case 4: CurrentScore += TetrisScore(level); break;
                default: break;
            }
        }

        /// <summary>
        /// Calculates the score for hard dropping a tetromino.
        /// </summary>
        /// <param name="nbLines">The ammount of lines by which the tetromino dropped.</param>
        public void HandleHardDropEvent(int nbLines)
        {
            CurrentScore += nbLines * 2;
        }

        /// <summary>
        /// Calculates the score for soft dropping a tetromino.
        /// </summary>
        public void HandleSoftDropEvent()
        {
            CurrentScore += 1;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(_font, string.Format("Score: {0}", CurrentScore.ToString()), 
                new Vector2(50, 400) + _offset, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
        }
    }
}
