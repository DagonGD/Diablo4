using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core
{
    /// <summary>
    /// Тип юнита
    /// </summary>
    public class UnitType
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name;
        /// <summary>
        /// Код
        /// </summary>
        public string Code;
        /// <summary>
        /// Максимум здоровья
        /// </summary>
        public int MaxHealth;
        /// <summary>
        /// Скорость передвижения (пикс/сек)
        /// </summary>
        public float Speed;

        #region Внешний вид
        /// <summary>
        /// Размеры изображения типа юнита
        /// </summary>
        public Vector2 Sizes;
        /// <summary>
        /// Количество сторон, в которые может быть повернут юнит
        /// </summary>
        public int FacesNum; 
        /// <summary>
        /// Изображение стоящего юнита
        /// </summary>
        public Texture2D Standing;
        /// <summary>
        /// Смещение начала ситемы координат юнита относительно системы координат карты
        /// </summary>
        public Vector2 Offset;
        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
