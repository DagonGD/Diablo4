using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Diablo4.Components
{
    class GameMap : DrawableGameComponent
    {
        private Map _map;
        private readonly Game _game;

        public GameMap(Game game) : base(game)
        {
            _game = game;
        }
        protected override void LoadContent()
        {
            _map = Map.Load("Worlds/World1/Map1.map");
            _map.Init(_game);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
                _map.Scroll.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            if (keyboardState.IsKeyDown(Keys.Down))
                _map.Scroll.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            if (keyboardState.IsKeyDown(Keys.Left))
                _map.Scroll.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            if (keyboardState.IsKeyDown(Keys.Right))
                _map.Scroll.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * 500;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //int i = _map.GetLandAtPos(_map.Screen2Map(new Vector2(mouseState.X, mouseState.Y)));
                _map.Units[0].Target=_map.Screen2Map(new Vector2(mouseState.X, mouseState.Y));
            }

            _map.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _map.Draw();
        }
    }
}
