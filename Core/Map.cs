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
                    coords = Map2Screen(new Vector2(i, j));
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
            Vector2 screen = Screen2Map(pos);
            int i = (int)screen.X;
            int j = (int)screen.Y;

            //Если координата попала на карту, возвращаем тип тайла
            if (i >= 0 && i < Width && j >= 0 && j < Height)
                return Land[i][j];
            
            //Координата за пределами карты
            return -1;
        }

        /// <summary>
        /// Преобразование экранных координат в координаты карты
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        /// <remarks>Накручено, зато работает правильно))</remarks>
        public Vector2 Screen2Map(Vector2 pos)
        {
            //http://www.gamedev.ru/code/forum/?id=122565#m7
            //http://www.math.by/geometry/eqline.html
            pos -= Scroll;

            //Определение координат красного робма
            int x = (int)Math.Floor(pos.X / Tile.Width);
            int y = (int)Math.Floor(pos.Y / Tile.Height);
            int i = y + x;
            int j = x - y;

            //Определение координаты в красном ромбе
            int a = ((int)Math.Floor(pos.X) % ((int)Math.Floor(Tile.Width)));
            int b = ((int)Math.Floor(pos.Y) % ((int)Math.Floor(Tile.Height)));

            if (a < 0)
                a = (int)Tile.Width + a;
            if (b < 0)
                b = (int)Tile.Height + b;

            //Определение части красного ромба, куда попали координаты
            if (Tile.Height * a / 2 + Tile.Width * b / 2 - Tile.Height * Tile.Width / 4 < 0)
                i--;

            if (-Tile.Height * a / 2 + Tile.Width * b / 2 + Tile.Height * Tile.Width / 4 < 0)
                j++;

            if (Tile.Height * a / 2 + Tile.Width * b / 2 - 6144 > 0)
                i++;

            if (-Tile.Height * a / 2 + Tile.Width * b / 2 - Tile.Height * Tile.Width / 4 > 0)
                j--;
            return new Vector2(i,j);
        }

        /// <summary>
        /// Преобразование координат карты в экранные
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Vector2 Map2Screen(Vector2 pos)
        {
            Vector2 coords;
            coords.X = Tile.Width * ((int)pos.X + (int)pos.Y) / 2;
            coords.Y = Tile.Height * ((int)pos.X - (int)pos.Y) / 2;

            return coords + Scroll;
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
