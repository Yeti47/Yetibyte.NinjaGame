using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileSetReference
    {
        public int FirstGid { get; private set; }
        public TileSet TileSet { get; private set; }
        public string TileSetPath { get; private set; }

        public bool IsTileSetLoaded => TileSet != null;

        private TileSetReference()
        {
        }

        public TileSetReference(int firstGid, TileSet tileSet)
        {
            FirstGid = firstGid;
            TileSet = tileSet ?? throw new ArgumentNullException(nameof(tileSet));
        }

        public TileSetReference(int firstGid, string tileSetPath)
        {
            FirstGid = firstGid;
            TileSetPath = tileSetPath;
        }

        public void LoadTileSet(ContentManager contentManager)
        {
            TileSet = contentManager?.Load<TileSet>(TileSetPath);
            TileSet?.LoadTexture(contentManager);
        }

        public int GetLocalTileId(int globalTileId)
        {
            return globalTileId - FirstGid + 1;
        }

    }
}
