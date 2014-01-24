using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    class Level
    {
        public int CurrentLevel { get; private set; }
        SpriteFont _font;
        public Level(SpriteFont font)
        {
            CurrentLevel = 1;
            _font = font;
        }
    }
}
