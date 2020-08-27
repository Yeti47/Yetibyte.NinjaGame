using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yetibyte.NinjaGame.Pipeline.TileMaps;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.Pipeline
{
    [ContentProcessor(DisplayName = "TileMap Processor")]
    public class TileMapProcessor : ContentProcessor<TileMapData, TileMap>
    {
        public const string TILE_SET_PATH = "TileSets";

        public override TileMap Process(TileMapData input, ContentProcessorContext context)
        {
            TileMap tileMap = new TileMap(input.TileWidth, input.TileWidth, input.Width, input.Height);
            
            foreach(var tsRef in input.TileSets)
            {
                string tsPath = $"{TILE_SET_PATH}/{Path.GetFileNameWithoutExtension(tsRef.Source)}";
                tileMap.AddTileSetReference(tsRef.FirstGid, tsPath);
            }

            foreach(var layer in input.Layers)
            {
                TileMatrix tileMatrix = TileMatrix.ParseCsv(layer.Data);
                tileMap.AddLayer(new TileMapTileLayer(tileMap, layer.Id, layer.Name, tileMatrix));

            }

            foreach(var objGroup in input.ObjectGroups)
            {
                TileMapObjectLayer objLayer = new TileMapObjectLayer();
                tileMap.AddLayer(objLayer);
            }

            return tileMap;

        }
    }
}
