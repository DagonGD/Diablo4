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
        #region Сериализуемые данные
        public int Width;
        public int Height;
        public int[][] Land;

        public List<Unit> Units;
        #endregion

        #region Ресурсы
        [XmlIgnore]
        [NonSerialized]
        public List<Tile> Tiles;
        [XmlIgnore]
        [NonSerialized]
        public List<UnitType> UnitTypes;
        #endregion

        #region Переменные
        [XmlIgnore]
        [NonSerialized]
        public SpriteBatch SpriteBatch;

        [XmlIgnore]
        [NonSerialized]
        private Game _game;

        [XmlIgnore]
        [NonSerialized]
        public Vector2 Scroll;
        #endregion

        #region Инициализация
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

            Units = new List<Unit>();
        }
        
        public void Init(Game game)
        {
            Tiles=new List<Tile>
                      {
                          new Tile(game.Content, "Images\\Tiles\\Land"),
                          new Tile(game.Content, "Images\\Tiles\\Water"),
                          new Tile(game.Content, "Images\\Tiles\\Brick")
                      };

            UnitTypes = new List<UnitType>
                            {
                                new UnitType {Name = "Воин", Code = "WARRIOR", MaxHealth = 100, Width = 50, Height = 100, Standing = game.Content.Load<Texture2D>("Images\\Units\\Warrior\\Standing")}
                            };

            foreach (Unit unit in Units)
            {
                unit.Map = this;
            }

            SpriteBatch=new SpriteBatch(game.GraphicsDevice);
            _game = game;
        }
        #endregion

        #region Отрисовка
        public void Draw()
        {
            Vector2 coords = Vector2.Zero;

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend/*, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone*/);
            
            #region Отрисовка карты
            for (int i=0; i<Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    coords.X = Tile.Width*(j+i)/2;
                    coords.Y = Tile.Height*(j-i)/2;

                    coords += Scroll;

                    if((coords.X+Tile.Width>0) && (coords.Y+Tile.Height>0) &&
                        (coords.X < _game.GraphicsDevice.Viewport.Width) && (coords.Y < _game.GraphicsDevice.Viewport.Height))
                        SpriteBatch.Draw(Tiles[Land[i][j]].Image, coords, Color.White);
                }
            }
            #endregion

            #region Отрисовка юнитов
            Units.ForEach(s => s.Draw());
            #endregion

            SpriteBatch.End();
        }
        #endregion

        #region Логика
        public void Update(GameTime gameTime)
        {
            Units.ForEach(s=>s.Update(gameTime));
        }

        /// <summary>
        /// Получение типа земли по экранным координатам
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetLandAtPos(Vector2 pos)
        {
            pos -= Scroll;

            pos -= new Vector2(Tile.Width/2, Tile.Height/2);
            
            //int i = (int) (pos.X/Tile.Width) + (int) (pos.Y/Tile.Height);
            //int j = (int) (pos.X/Tile.Width) - (int) (pos.Y/Tile.Height);

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Vector2 coords=new Vector2(Tile.Width*(j + i)/2, Tile.Height*(j - i)/2);
                    if (pos.X > coords.X && pos.X < coords.X + Tile.Width && pos.Y > coords.Y && pos.Y < coords.Y + Tile.Height)
                    {

                        if (i < 0 || i >= Width || j < 0 || j >= Height)
                            return -1;

                        return Land[i][j];
                    }
                }
            }

            return -1;
        }
        #endregion

        #region Сериализация
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
        #endregion
    }
}
