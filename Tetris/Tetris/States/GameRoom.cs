using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JNetwork;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarshalHelper;

namespace Tetris.States
{
    class GameRoom : GameState
    {
        UDPServer _server = new UDPServer();
        Client _client;
        Client _host;
        string message = "";
        SpriteFont _font;
        public GameRoom(StateManager manager)
            : base(manager)
        {
            LoadContent();
            MarshalHelper.DataStruct test = new MarshalHelper.DataStruct();
            test.action = "reply";
            test.name = "Prismik";
            test.x = 0;
            test.y = 0;
            _server.addAction("sendText", new ThreadStart(delegate { _server.sendStruct(test, "Testing the function"); }));
            _server.startServer();
            _host = new Client("Prismik");
            _host.sendStruct("sendText", "Prismik", 10, 21);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            message = _host.LastReceived;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("gamefont");
        }
        
        public override void Draw()
        {
            Manager.SpriteBatch.Begin();
            Manager.SpriteBatch.DrawString(_font, message, new Microsoft.Xna.Framework.Vector2(150, 150), Color.Orange);
            Manager.SpriteBatch.End();
        }
    }
}
