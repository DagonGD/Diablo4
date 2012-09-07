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
            Map map=new Map(5,5);
            map.Save("map1.map");
        }
    }
}
