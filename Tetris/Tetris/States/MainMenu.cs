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
            }, new Vector2(250, 50), _font);

            MenuNode multiPlayerNode = new ColoredMenuNode("Multi", "Local Multi Player", delegate
            {
                Manager.AddState(new LocalMultiplayer(Manager));
                Manager.RemoveState(this);
            }, new Vector2(250, 150), _font);

            MenuNode udpNode = new ColoredMenuNode("UDP", "UDP Server Test", delegate
            {
                Manager.AddState(new GameRoom(Manager));
                Manager.RemoveState(this);
            }, new Vector2(250, 250), _font);

            MenuNode t4 = new ColoredMenuNode("Multi", "Local Multi Player", delegate
            {
                Manager.AddState(new LocalMultiplayer(Manager));
                Manager.RemoveState(this);
            }, new Vector2(250, 350), _font);

            singlePlayerNode.Bottom = multiPlayerNode;
            singlePlayerNode.Top = t4;

            multiPlayerNode.Bottom = udpNode;
            multiPlayerNode.Top = singlePlayerNode;

            udpNode.Bottom = t4;
            udpNode.Top = multiPlayerNode;

            t4.Bottom = singlePlayerNode;
            t4.Top = udpNode;

            nodes.AddRange(new MenuNode[] {singlePlayerNode, multiPlayerNode, udpNode, t4});
            _menu = new ColoredMenu(nodes, _font, Manager.SpriteBatch, InputState);
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
