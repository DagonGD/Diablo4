using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Core
{
    public static class Utils
    {
        /// <summary>
        /// Получение угла между вектором и осью Y
        /// </summary>
        /// <param name="v">Вектор</param>
        /// <returns>Угол</returns>
        public static double GetAngle(Vector2 v)
        {
            double angle;
            //Если угол равен ±90°
            if (Math.Abs(v.X) < float.Epsilon)
            {
                angle = Math.PI / 2 * Math.Sign(v.Y);
            }
            else
            {
                double atan = -v.Y / v.X;
                angle = Math.Atan(atan);
                if (v.X < 0)
                    angle = Math.PI + angle;
                if (v.X > 0 && v.Y > 0)
                    angle = 2 * Math.PI + angle;

            }

            return angle;
        }

        /// <summary>
        /// Нормализация угла (между 0 и 360)
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double NormalizeAngle(double angle)
        {
            while (angle < 0)
                angle += 2 * Math.PI;
            while (angle > 2 * Math.PI)
                angle -= 2 * Math.PI;
            return angle;
        }
    }
}
