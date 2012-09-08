using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core
{
    public class Tile
    {
        public static readonly float Width = 128;
        public static readonly float Height = 64;

        public Texture2D Image;

        public Tile()
        {
            
        }

        public Tile(ContentManager content, string filename)
        {
            Load(content, filename);
        }

        public void Load(ContentManager content, string filename)
        {
            Image = content.Load<Texture2D>(filename);

            if (Image.Height != (int)Height || Image.Width != (int)Width)
                throw new FormatException("Неверные размеры тайла");
        }
    }
}
