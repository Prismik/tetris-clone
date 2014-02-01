using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Tetris.States
{
    class LocalMultiplayer: GameState
    {
        Board _firstBoard, _secondBoard;
    
        SpriteFont _font;
        Texture2D _texture;
        Texture2D _ghost;
        public LocalMultiplayer(StateManager manager)
            :base(manager)
        {
            LoadContent();
            ControlsConfig left = InitLeftControl();
            ControlsConfig right = InitRightControl();
            _firstBoard = new Board(Manager.SpriteBatch, _font, _texture, _ghost, Manager.Game, InputState, new Vector2(50, 0), PlayerIndex.One, left);
            _secondBoard = new Board(Manager.SpriteBatch, _font, _texture, _ghost, Manager.Game, InputState, new Vector2(550, 0), PlayerIndex.Two, right);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _font = Manager.Game.Content.Load<SpriteFont>("gamefont");
            _texture = Manager.Game.Content.Load<Texture2D>("block");
            _ghost = Manager.Game.Content.Load<Texture2D>("ghost");
        }

        private ControlsConfig InitLeftControl()
        {
            ControlsConfig config = new ControlsConfig();
            config.Left = Microsoft.Xna.Framework.Input.Keys.Left;
            config.Bottom = Microsoft.Xna.Framework.Input.Keys.Down;
            config.Right = Microsoft.Xna.Framework.Input.Keys.Right;
            config.Rotate = Microsoft.Xna.Framework.Input.Keys.Space;
            config.Hold = Microsoft.Xna.Framework.Input.Keys.LeftShift;
            config.Drop = Microsoft.Xna.Framework.Input.Keys.D;

            return config;
        }

        private ControlsConfig InitRightControl()
        {
            ControlsConfig config = new ControlsConfig();
            config.Left = Microsoft.Xna.Framework.Input.Keys.Left;
            config.Bottom = Microsoft.Xna.Framework.Input.Keys.Down;
            config.Right = Microsoft.Xna.Framework.Input.Keys.Right;
            config.Rotate = Microsoft.Xna.Framework.Input.Keys.Space;
            config.Hold = Microsoft.Xna.Framework.Input.Keys.LeftShift;
            config.Drop = Microsoft.Xna.Framework.Input.Keys.D;

            return config;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _firstBoard.Update(gameTime);
            _secondBoard.Update(gameTime);
        }

        public override void Draw()
        {
            _firstBoard.Draw();
            _secondBoard.Draw();
        }
    }
}
