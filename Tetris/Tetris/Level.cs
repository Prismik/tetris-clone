using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;
using System.IO;

namespace Tetris
{
    class Level
    {
        private const int MAX_DELAYS = 12;
        private const string INFO_FILE_PATH = "Content/info.txt";
        public int CurrentLevel { get; private set; }
        public float CurrentDelay { get; private set; }
        SpriteFont _font;
        Vector2 _offset;
        public Level(SpriteFont font, Vector2 location)
        {
            _offset = location;
            CurrentLevel = 0;
            _font = font;
            LevelUp();
        }

        public void LevelUp()
        {
            CurrentLevel++;
            ReadLevelInfoFromFile();
        }

        private void ReadLevelInfoFromFile()
        {
            string line = null;
            using (Stream stream = File.Open(INFO_FILE_PATH, FileMode.Open))
                using (StreamReader reader = new StreamReader(stream))
                    for (int i = 0; i != (CurrentLevel > MAX_DELAYS ? MAX_DELAYS : CurrentLevel); ++i)
                        line = reader.ReadLine();

            CurrentDelay = int.Parse(line);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(_font, string.Format("Level: {0}", CurrentLevel.ToString()), 
                new Vector2(50, 350) + _offset, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
        }
    }
}
