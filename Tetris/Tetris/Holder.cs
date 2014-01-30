using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tetris.Entities.Tetromino;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class Holder
    {
        public bool Active { get; set; }
        public Tetromino PreviousHeld { get; protected internal set; }
        public Tetromino Held { get; protected internal set; }
        private SpriteFont _font;
        Vector2 _offset;
        public Holder(SpriteFont font, Vector2 location)
        {
            _offset = location;
            PreviousHeld = null;
            Held = null;
            Active = true;
            _font = font;
        }

        public Tetromino Hold(Tetromino hold)
        {
            // If hold has just been held (just in case it would happen)
            if (PreviousHeld == hold)
                throw new ArgumentException("Tetormino Hold");

            PreviousHeld = Held;
            Held = hold;

            Active = false;
            return PreviousHeld;
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sb.DrawString(_font, "Holder", new Vector2(255, 0) + _offset, Color.White);
            if (Held != null)
                Held.Normalize().Draw(sb, texture, (int)_offset.X + 255, (int)_offset.Y + 50);
        }
    }
}
