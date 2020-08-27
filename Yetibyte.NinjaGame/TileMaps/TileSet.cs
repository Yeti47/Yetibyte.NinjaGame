using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileSet
    {
        public string Name { get; set; }

        public int TileWidth { get; private set; }
        public int TileHeigth { get; private set; }

        public Texture2D Texture { get; private set; }

        public string TexturePath { get; private set; }

        public int ColumnCount { get; private set; }
        public int TileCount { get; private set; }

        public int RowCount => TileCount / ColumnCount;

        public TileSet(string name, int tileWidth, int tileHeigth, string texturePath, int columnCount, int tileCount)
        {
            Name = name;
            TileWidth = tileWidth;
            TileHeigth = tileHeigth;
            //Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            TexturePath = texturePath;
            ColumnCount = columnCount;
            TileCount = tileCount;
        }

        private TileSet()
        {

        }

        public Rectangle GetTileBounds(int x, int y)
        {
            return new Rectangle
            {
                X = x * TileWidth,
                Y = y * TileHeigth,
                Width = TileWidth,
                Height = TileHeigth
            };

        }

        public Rectangle GetTileBoundsByLocalId(int tileId)
        {
            int x = (tileId - 1) % ColumnCount;
            int y = (tileId - 1) / ColumnCount;

            return GetTileBounds(x, y);

        }

        public bool LoadTexture(ContentManager contentManager)
        {
            if (contentManager is null)
                throw new ArgumentNullException(nameof(contentManager));

            Texture = contentManager.Load<Texture2D>(this.TexturePath.Trim(new[] { '\\', '/' }));

            return Texture != null;

        }

    }

}
