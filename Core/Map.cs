using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Core
{
    public class Map
    {
        public int Width;
        public int Height;

        public int[][] Land;

        public Map()
        {
            
        }

        public Map(int w, int h)
        {
            Width = w;
            Height = h;
            Land = new int[w][];

            for(int i=0;i<w;i++)
            {
                Land[i]=new int[h];
                for(int j=0;j<h;j++)
                {
                    Land[i][j] = 0;
                }
            }
        }

        public static Map Load(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));

            using(FileStream f=File.Open(filename, FileMode.Open))
            {
                return (Map)serializer.Deserialize(f);
            }
        }

        public void Save(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Map));

            using (FileStream f = File.Open(filename, FileMode.Create))
            {
                serializer.Serialize(f, this);
            }
        }
    }
}
