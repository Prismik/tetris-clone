using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class Score
    {
        public int CurrentScore { get; private set; }
        SpriteFont _font;
        public Score(SpriteFont font)
        {
            CurrentScore = 0;
            _font = font;
        }

        /// <summary>
        /// Calculates the score for clearing a single line.
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The ammount of points added to the current score.</returns>
        private int SingleScore(int level)
        {
            return level * 100;
        }

        /// <summary>
        /// Calculates the score for clearing a single line.
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The ammount of points added to the current score.</returns>
        private int DoubleScore(int level)
        {
            return level * 300;
        }

        /// <summary>
        /// Calculates the score for clearing a two lines.
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The ammount of points added to the current score.</returns>
        private int TripleScore(int level)
        {
            return level * 500;
        }

        /// <summary>
        /// Calculates the score for clearing three lines.
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The ammount of points added to the current score.</returns>
        private int TetrisScore(int level)
        {
            return level * 800;
        }

        /// <summary>
        /// Calculates the score for clearing a four lines.
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The ammount of points added to the current score.</returns>
        public void HandleScoreEvent(int nbLines, int level)
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

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(_font, string.Format("Score: {0}", CurrentScore.ToString()), 
                new Vector2(50, 400), Color.Black, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
        }
    }
}
