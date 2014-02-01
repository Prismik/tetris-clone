using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris.Menu
{
    abstract class Menu
    {
        List<MenuNode> _nodes = new List<MenuNode>();
        MenuNode _currentNode;
        public Menu(List<MenuNode> linkedNodes)
        {
            _nodes = linkedNodes;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (MenuNode node in _nodes)
                node.Update(gameTime);
        }

        public virtual void Draw()
        {
            foreach (MenuNode node in _nodes)
                node.Draw();
        }
    }
}
