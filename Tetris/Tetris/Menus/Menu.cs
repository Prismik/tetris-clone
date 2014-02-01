using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Menus
{
    public class Menu
    {
        SpriteFont _font;
        SpriteBatch _spriteBatch;
        InputState _input;
        List<MenuNode> _nodes = new List<MenuNode>();
        public MenuNode CurrentNode { get; private set; }
        public Menu(List<MenuNode> linkedNodes, SpriteFont font, SpriteBatch sb, InputState input)
        {
            _input = input;
            _font = font;
            _nodes = linkedNodes;
            _spriteBatch = sb;
            CurrentNode = _nodes.First();
            CurrentNode.Active = true;
        }

        private void ChangeActive(MenuNode newNode)
        {
            CurrentNode.Active = false;
            newNode.Active = true;
            CurrentNode = newNode;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (MenuNode node in _nodes)
                node.Update(gameTime);

            PlayerIndex useless;
            if (_input.IsMenuSelect(null, out useless))
                CurrentNode.Action();
            else if (_input.IsMenuDown(null))
                ChangeActive(CurrentNode.Bottom);
            else if (_input.IsMenuUp(null))
                ChangeActive(CurrentNode.Top);
        }

        public virtual void Draw()
        {
            foreach (MenuNode node in _nodes)
                node.Draw(_spriteBatch, _font);
        }
    }
}
