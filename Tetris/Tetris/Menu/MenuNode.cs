using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;

namespace Tetris.Menu
{
    abstract class MenuNode
    {
        public string ID { get; private set; }
        Keys _actionKey;
        Action _actionOnPress;
        MenuNode Top { get; set; }
        MenuNode Right { get; set; }
        MenuNode Bottom { get; set; }
        MenuNode Left { get; set; }
        public MenuNode(string id, string text, Action action)
        {
            _actionOnPress = action;
            ID = id;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw()
        {

        }
    }
}
