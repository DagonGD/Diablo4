using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Microsoft.Xna.Framework;

namespace CreateResources
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map=new Map(50,50);

            for (int i = 0; i < map.Width; i++)
                map.Land[i][2] = 1;

            for (int j = 0; j < map.Height; j++)
                map.Land[25][j] = 2;

            map.Units.Add(new Unit { TypeCode = "WARRIOR", Position = new Vector2(500f,0f), Health = 100, Map = map });

            map.Save("map1.map");
        }
    }
}
