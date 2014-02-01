using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris.Menus
{
    class ColoredMenuNode : MenuNode
    {
        string _text;
        Color _color;
        public ColoredMenuNode(string id, string text, Action action, Vector2 position)
            :base(id, action, position)
        {
            _text = text;
            _color = Color.White;
        }

        internal override void OnToggleActive(bool value)
        {
            _color = (value == true ? Color.Red : Color.White);
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb, Microsoft.Xna.Framework.Graphics.SpriteFont font)
        {
            sb.DrawString(font, _text, Position, _color);
        }
    }
}
