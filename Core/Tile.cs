using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core
{
    /// <summary>
    /// Тайл земли
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// 128
        /// </summary>
        public static readonly float Width = 128;
        /// <summary>
        /// 64
        /// </summary>
        public static readonly float Height = 64;
        /// <summary>
        /// (128,64)
        /// </summary>
        public static readonly Vector2 Sizes = new Vector2(Width, Height);

        public Texture2D Image;

        public bool IsPassable;

        public Tile()
        {
            
        }

        public Tile(ContentManager content, string filename, bool isPassable)
        {
            Load(content, filename);
            IsPassable = isPassable;
        }

        public void Load(ContentManager content, string filename)
        {
            Image = content.Load<Texture2D>(filename);

            if (Image.Height != (int)Height || Image.Width != (int)Width)
                throw new FormatException("Неверные размеры тайла");
        }
    }
}
