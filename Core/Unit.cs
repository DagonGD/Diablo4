using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Core
{
    [Serializable]
    public class Unit
    {
        private UnitType _type;
        [XmlIgnore]
        public UnitType Type
        {
            get
            {
                if(_type==null && !string.IsNullOrEmpty(TypeCode) && Map!=null)
                {
                    _type = Map.UnitTypes.FirstOrDefault(u => u.Code == TypeCode);
                }
                return _type;
            }
        }
        public string TypeCode;

        [XmlIgnore]
        [NonSerialized]
        public Map Map;

        public int Health;
        public Vector2 Position;
        public int Angle;

        private double _lastRotationTime;

        public void Draw()
        {
            Rectangle source = new Rectangle(Type.Width*Angle, 0, Type.Width, Type.Height);
            Map.SpriteBatch.Draw(Type.Standing, Position + Map.Scroll, source, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds-_lastRotationTime>3000)
            {
                _lastRotationTime = gameTime.TotalGameTime.TotalMilliseconds;
                if (++Angle > 3)
                    Angle = 0;

            }
        }
    }
}
