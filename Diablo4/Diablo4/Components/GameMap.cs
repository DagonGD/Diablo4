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
        public Map Map;
        private readonly Game _game;

        public GameMap(Game game) : base(game)
        {
            _game = game;
        }
        protected override void LoadContent()
        {
            Map = Map.Load("Worlds/World1/Map1.map");
            Map.Init(_game);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
                Map.Scroll.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            if (keyboardState.IsKeyDown(Keys.Down))
                Map.Scroll.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            if (keyboardState.IsKeyDown(Keys.Left))
                Map.Scroll.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 500;
            if (keyboardState.IsKeyDown(Keys.Right))
                Map.Scroll.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * 500;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //int i = _map.GetLandAtPos(_map.Screen2Map(new Vector2(mouseState.X, mouseState.Y)));
                Map.Units[0].Target=Map.Screen2Map(new Vector2(mouseState.X, mouseState.Y));
            }

            Map.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Map.Draw();
        }
    }
}
