using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Menus
{
    public abstract class MenuNode
    {
        Keys _actionKey;
        public string ID { get; private set; }
        public Action Action { get; set; }
        public MenuNode Top { get; set; }
        public MenuNode Right { get; set; }
        public MenuNode Bottom { get; set; }
        public MenuNode Left { get; set; }
        bool _active;
        public bool Active { get { return _active; } set { OnToggleActive(value); _active = value; } }
        public Vector2 Position { get; private set; }
        public MenuNode(string id, Action action, Vector2 position)
        {
            Position = position;
            Action = action;
            ID = id;
        }

        internal abstract void OnToggleActive(bool value);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch sb, SpriteFont font);
    }
}
