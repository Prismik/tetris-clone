using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tetris.Menus;

namespace Tetris.States
{
    class MainMenu : GameState
    {
        Menu _menu;
        SpriteFont _font;
        public MainMenu(StateManager manager)
            :base(manager)
        {
            LoadContent();
            InitMenu();
        }

        public void InitMenu()
        {
            List<MenuNode> nodes = new List<MenuNode>();
            MenuNode singlePlayerNode = new ColoredMenuNode("Single", "Single Player", delegate
            {
                Manager.AddState(new SinglePlayer(Manager));
                Manager.RemoveState(this);
            }, new Vector2(50, 50));

            MenuNode multiPlayerNode = new ColoredMenuNode("Multi", "Local Multi Player", delegate
            {
                Manager.AddState(new LocalMultiplayer(Manager));
                Manager.RemoveState(this);
            }, new Vector2(50, 150));

            singlePlayerNode.Bottom = multiPlayerNode;
            singlePlayerNode.Top = multiPlayerNode;

            multiPlayerNode.Bottom = singlePlayerNode;
            multiPlayerNode.Top = singlePlayerNode;

            nodes.AddRange(new MenuNode[] {singlePlayerNode, multiPlayerNode});
            _menu = new Menu(nodes, _font, Manager.SpriteBatch, InputState);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("gamefont");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _menu.Update(gameTime);
        }

        public override void Draw()
        {
            Manager.SpriteBatch.Begin();
            _menu.Draw();
            Manager.SpriteBatch.End();
        }
    }
}
