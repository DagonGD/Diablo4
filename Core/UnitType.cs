using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Core
{
    public class UnitType
    {
        public string Name;
        public string Code;
        public int MaxHealth;

        #region Внешний вид
        public int Width;
        public int Height;
        public Texture2D Standing;
        #endregion
    }
}
