using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Core
{
    /// <summary>
    /// Юнит
    /// </summary>
    [Serializable]
    public class Unit
    {
        private UnitType _type;
        /// <summary>
        /// Тип юнита
        /// </summary>
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
        /// <summary>
        /// Код типа юнита
        /// </summary>
        public string TypeCode;

        /// <summary>
        /// Карта на которой размещен юнит
        /// </summary>
        [XmlIgnore]
        [NonSerialized]
        public Map Map;

        /// <summary>
        /// Количество здоровья
        /// </summary>
        public int Health;
        /// <summary>
        /// Расположение на карте
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Угол в радианах, на который повернут юнит относительно оси Y против часовой стрелки
        /// </summary>
        private double _angle ;
        public double Angle
        {
            set { _angle = Utils.NormalizeAngle(value); }
            get { return _angle; }
        }
        /// <summary>
        /// Угол в градусах, на который повернут юнит относительно оси Y против часовой стрелки
        /// </summary>
        [XmlIgnore]
        public double AngleDeg
        {
            get { return Angle*180d/Math.PI; }
        }

        public Vector2 Target;

        /// <summary>
        /// Нарисовать юнит
        /// </summary>
        public void Draw()
        {
            int srcOffset = (int)(Utils.NormalizeAngle(Angle + Math.PI / Type.FacesNum) / (2 * Math.PI / Type.FacesNum));
            if(srcOffset<0 || srcOffset>=Type.FacesNum)
                throw new IndexOutOfRangeException();

            Rectangle source = new Rectangle((int)Type.Sizes.X * srcOffset, 0, (int)Type.Sizes.X, (int)Type.Sizes.Y);
            Vector2 drawPos = Position + Map.Scroll - Type.Offset;

            if (drawPos.X + Type.Sizes.X > 0 && drawPos.X < Map.Game.GraphicsDevice.Viewport.Width &&
                drawPos.Y + Type.Sizes.Y > 0 && drawPos.Y < Map.Game.GraphicsDevice.Viewport.Height)
                Map.SpriteBatch.Draw(Type.Standing, drawPos, source, Color.White);
        }

        /// <summary>
        /// Логика работы
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
           UpdatePosition(gameTime);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Vector2 delta = Target - Position;
            //Если идти некуда или скорость нулевая
            if(delta==Vector2.Zero || Math.Abs(Type.Speed)<float.Epsilon)
                return;
            
            if (delta.Length() < Type.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds)
            {
                //Пришли
                if (Map.IsPassable(Target))
                    Position = Target;
                else
                    Target = Position;
            }
            else
            {
                //Разворачиваемся к цели
                RotateTo(Target);
                //Новая позиция
                Vector2 newPosition = Position + Vector2.Normalize(delta) * Type.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //Если можно пройти
                if (Map.IsPassable(newPosition))
                    //Идем
                    Position = newPosition;
                else
                    //Иначе стоим
                    Target = Position;
            }
        }

        /// <summary>
        /// Разворот юнита в сторону указанной точки
        /// </summary>
        /// <param name="point">Координаты точки в относительно карты игры</param>
        public void RotateTo(Vector2 point)
        {
            Vector2 delta = point - Position;
            Angle = Utils.GetAngle(delta);
        }

        public override string ToString()
        {
            return Type.Name;
        }
    }
}
