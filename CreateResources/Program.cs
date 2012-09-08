using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace CreateResources
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map=new Map(50,50);
            for (int i = 0; i < map.Width; i++)
                map.Land[i][2] = 1;
            map.Save("map1.map");
        }
    }
}
