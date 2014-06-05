using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Menus
{
    public abstract class Menu
    {
        SpriteFont _font;
        SpriteBatch _spriteBatch;
        InputState _input;
        List<MenuNode> _nodes = new List<MenuNode>();
        bool _init = false;
        bool _leaving = false;

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

        /// <summary>
        /// Changes the active menu node.
        /// </summary>
        /// <param name="newNode">The new node to be active.</param>
        protected virtual void ChangeActive(MenuNode newNode)
        {
            CurrentNode.Active = false;
            newNode.Active = true;
            CurrentNode = newNode;
        }

        protected virtual void EnterContext(MenuNode node)
        {
            _init = true;
            node.EnterContext(pass, 1);
        }

        protected virtual void LeaveContext(MenuNode node, int pos)
        {
            node.LeaveContext(pass, pos);
            pass += 0.1f;
        }

        protected abstract bool LeaveContextDone(MenuNode node);
        protected abstract bool EnterContextDone(MenuNode node);

        float pass = 0.3f;
        public virtual void Update(GameTime gameTime)
        {
            int i = 0;
            bool doneLeave = false;
            foreach (MenuNode node in _nodes)
            {
                node.Update(gameTime);

                if (!_init)
                {
                    EnterContext(node);
                    EnterContextDone(node);
                }

                if (_leaving)
                {
                    LeaveContext(node, i++);
                    doneLeave = LeaveContextDone(node);
                }
            }

            if (doneLeave)
                CurrentNode.Action();

            PlayerIndex useless;
            if (_init && !_leaving)
            {
                if (_input.IsMenuSelect(null, out useless))
                    _leaving = true;
                else if (_input.IsMenuDown(null))
                    ChangeActive(CurrentNode.Bottom);
                else if (_input.IsMenuUp(null))
                    ChangeActive(CurrentNode.Top);
            }
        }
        public virtual void Draw()
        {
            foreach (MenuNode node in _nodes)
                node.Draw(_spriteBatch, _font);
        }
    }
}
