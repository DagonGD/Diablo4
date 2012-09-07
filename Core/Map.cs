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
        public int Height;
        public int Width;

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
