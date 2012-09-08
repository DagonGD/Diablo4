using Microsoft.Xna.Framework;

namespace Diablo4.Components
{
    // ReSharper disable InconsistentNaming
    class FPSCounter : DrawableGameComponent
    // ReSharper restore InconsistentNaming
    {
        public int Fps;
        int _frames;
        double _seconds;
        private Game _game;

        public FPSCounter(Game game)
            : base(game)
        {
            _game = game;
        }

        public override void Update(GameTime gameTime)
        {
            _seconds += gameTime.ElapsedGameTime.TotalSeconds;

            if (_seconds >= 1)
            {
                Fps = _frames;
                _seconds = 0;
                _frames = 0;
                _game.Window.Title = Fps.ToString();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _frames++;
            
            base.Draw(gameTime);
        }
    }
}