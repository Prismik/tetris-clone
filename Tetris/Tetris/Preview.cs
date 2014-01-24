using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Entities.Tetromino;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
    /// <summary>
    /// Represents the previews of the tetrominos to come.
    /// </summary>
    class Preview
    {
        Queue<Tetromino> _previews;
        SpriteFont _font;
        TetrominoBuffer _buffer;
        public Preview(SpriteFont font, TetrominoBuffer buffer)
        {
            _font = font;
            _previews = new Queue<Tetromino>(3);
            _buffer = buffer;
        }

        /// <summary>
        /// Removes the last tetromino and adds the next one to the preview list.
        /// </summary>
        public void Next()
        {
            _previews.Clear();
            List<int> buff = (List<int>)_buffer.Peek(3);
            foreach (int code in buff)
                _previews.Enqueue(new Tetromino((TetrominoTypes)code)); // Could optimize here
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            int i = 0;
            foreach (Tetromino t in _previews)
            {
                t.Normalize().Draw(sb, texture, 255, 320 - i * 50);
                i++;
            }
        }
    }
}
