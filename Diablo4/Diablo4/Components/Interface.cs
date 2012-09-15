using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diablo4.Components
{
    class Interface : DrawableGameComponent
    {
        private readonly Game _game ;
        private SpriteFont _font1;
        private SpriteBatch _spriteBatch;
        private FPSCounter _fpsCounter;
        private Map _map;

        public Interface(Game game, FPSCounter fpsCounter=null) : base(game)
        {
            _game = game;
            _fpsCounter = fpsCounter;
            //_map = map;
        }

        protected override void LoadContent()
        {
            _font1 = _game.Content.Load<SpriteFont>("Fonts\\Arial");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font1, "Health: 100", new Vector2(0, 0), Color.Black);
            if(_fpsCounter!=null)
                _spriteBatch.DrawString(_font1, "FPS: "+_fpsCounter.Fps.ToString(), new Vector2(0, 15), Color.Black);
            _spriteBatch.End();
        }
    }
}
