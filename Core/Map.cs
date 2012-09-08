using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core
{
    [Serializable]
    public class Map
    {
        public int Width;
        public int Height;

        public float ScrollX;
        public float ScrollY;

        public int[][] Land;

        [NonSerialized]
        public List<Tile> Tiles;

        private SpriteBatch _spriteBatch;
        private Game _game;



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

        public void Init(Game game)
        {
            Tiles=new List<Tile>();
            Tiles.Add(new Tile(game.Content, "Images\\Tiles\\Land"));
            Tiles.Add(new Tile(game.Content, "Images\\Tiles\\Water"));
            Tiles.Add(new Tile(game.Content, "Images\\Tiles\\Brick"));

            _spriteBatch=new SpriteBatch(game.GraphicsDevice);
            _game = game;
        }

        public void Draw()
        {
            Vector2 coords = Vector2.Zero;

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            for(int i=0; i<Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    coords.X = i*Tile.Width/2 + j*Tile.Width/2 + ScrollX;
                    coords.Y = j*Tile.Height/2 - i*Tile.Height/2 + ScrollY;

                    if((coords.X+Tile.Width>0) && (coords.Y+Tile.Height>0) &&
                        (coords.X < _game.GraphicsDevice.Viewport.Width) && (coords.Y < _game.GraphicsDevice.Viewport.Height))
                        _spriteBatch.Draw(Tiles[Land[i][j]].Image, coords, Color.White);
                }
            }

            _spriteBatch.End();
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
