using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.ContentReaders
{
    public class TileMapReader : ContentTypeReader<TileMap>
    {
        protected override TileMap Read(ContentReader input, TileMap existingInstance)
        {
            if (existingInstance != null)
                return existingInstance;

            int tileWidth = input.ReadInt32();
            int tileHeight = input.ReadInt32();
            int width = input.ReadInt32(); 
            int height = input.ReadInt32();

            var tileSetRefs = ReadTileSetReferences(input);

            TileMap map = new TileMap(tileHeight, tileWidth, width, height);

            var layers = ReadLayers(input, map);

            foreach(var tsRef in tileSetRefs)
            {
                map.AddTileSetReference(tsRef);
            }

            foreach(var layer in layers)
            {
                map.AddLayer(layer);
            }

            return map;

        }

        private IEnumerable<TileSetReference> ReadTileSetReferences(ContentReader input)
        {
            List<TileSetReference> tsRefs = new List<TileSetReference>();

            int tileSetRefCount = input.ReadInt32();

            for(int i = 0; i < tileSetRefCount; i++)
            {
                int firstGid = input.ReadInt32();
                string path = input.ReadString();

                tsRefs.Add(new TileSetReference(firstGid, path));

            }

            return tsRefs;

        }

        private IEnumerable<TileMapTileLayer> ReadLayers(ContentReader input, TileMap tilemap)
        {
            int layerCount = input.ReadInt32();

            List<TileMapTileLayer> tileLayers = new List<TileMapTileLayer>();

            for (int i = 0; i < layerCount; i++)
            {
                int id = input.ReadInt32();
                string name = input.ReadString();

                int width = input.ReadInt32();
                int height = input.ReadInt32();

                var matrix = ReadTileMatrix(input, width, height);

                TileMapTileLayer layer = new TileMapTileLayer(tilemap, id, name, matrix);

                tileLayers.Add(layer);

            }

            return tileLayers;

        }

        private TileMatrix ReadTileMatrix(ContentReader input, int layerWidth, int layerHeight)
        {
            List<TileMapTile> tileCache = new List<TileMapTile>();

            TileMatrix matrix = new TileMatrix(layerWidth, layerHeight);

            for (int i = 0; i < layerWidth * layerHeight; i++)
            {
                int tileId = input.ReadInt32();

                var tile = tileCache.FirstOrDefault(t => t.Id == tileId);

                if(tile == null)
                {
                    tile = new TileMapTile(tileId);
                    tileCache.Add(tile);
                }

                matrix.SetTile(i % layerWidth, i / layerWidth, tile);

            }

            return matrix;

        }

    }
}
