using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yetibyte.NinjaGame.TileMaps
{

    public class TileMapTileLayer : TileMapLayer
    {

        public int Height => TileMatrix.Height;
        public int Width => TileMatrix.Width;

        public override TileMapLayerType LayerType => TileMapLayerType.Tile;

        public TileMatrix TileMatrix { get; private set; }

        public TileMapTileLayer(TileMap parentTileMap, int id, string name, int width, int height) :
            base(parentTileMap, id, name)
        {
            TileMatrix = new TileMatrix(width, height);

        }

        public TileMapTileLayer(TileMap parentTileMap, int id, string name, TileMatrix tileMatrix)
            : base(parentTileMap, id, name)
        {
            TileMatrix = tileMatrix;
        }


    }
}
