using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Yetibyte.NinjaGame.Core;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.ContentReaders
{
    public class TileMapReader : ContentTypeReader<TileMap>
    {

        private class SharedObjectData
        {
            public float X { get; set; }
            public float Y { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
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
            var objLayers = ReadObjectLayers(input, map);

            foreach(var tsRef in tileSetRefs)
            {
                map.AddTileSetReference(tsRef);
            }

            foreach(var layer in layers)
            {
                map.AddLayer(layer);
            }

            foreach (var objLayer in objLayers)
            {
                map.AddLayer(objLayer);
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

        private IEnumerable<TileMapObjectLayer> ReadObjectLayers(ContentReader input, TileMap tileMap)
        {

            List<TileMapObjectLayer> layers = new List<TileMapObjectLayer>();

            int objLayerCount = input.ReadInt32();

            for(int i = 0; i < objLayerCount; i++)
            {
                int layerId = input.ReadInt32();
                string layerName = input.ReadString();

                TileMapObjectLayer tileMapObjectLayer = new TileMapObjectLayer(tileMap, layerId, layerName);

                int pointObjectCount = input.ReadInt32();

                for(int j = 0; j < pointObjectCount; j++)
                {
                    var sharedData = ReadSharedObjectData(input);

                    TileMapPointObject pointObj = new TileMapPointObject(sharedData.Name, sharedData.Type, sharedData.Id, sharedData.X, sharedData.Y);

                    ReadProperyMap(input, pointObj.CustomProperties);

                    tileMapObjectLayer.AddObject(pointObj);

                }

                int polygonObjectCount = input.ReadInt32();

                for(int j = 0; j < polygonObjectCount; j++)
                {
                    var sharedData = ReadSharedObjectData(input);

                    PropertyMap propMap = new PropertyMap();

                    ReadProperyMap(input, propMap);

                    List<Vector2> vertices = new List<Vector2>();

                    int vertexCount = input.ReadInt32();

                    for(int v = 0; v < vertexCount; v++)
                    {
                        Vector2 vertex = input.ReadVector2();
                        vertices.Add(vertex);
                    }

                    TileMapPolygonObject tileMapPolygon = new TileMapPolygonObject(vertices, sharedData.Name, sharedData.Type, sharedData.Id, sharedData.X, sharedData.Y);

                    foreach (var kvp in propMap)
                        tileMapPolygon.CustomProperties.Add(kvp);

                    tileMapObjectLayer.AddObject(tileMapPolygon);

                }

                int rectObjectCount = input.ReadInt32();

                for(int j = 0; j < rectObjectCount; j++)
                {
                    var sharedData = ReadSharedObjectData(input);

                    PropertyMap propMap = new PropertyMap();

                    ReadProperyMap(input, propMap);

                    float width = input.ReadSingle();
                    float height = input.ReadSingle();

                    TileMapRectangleObject rectangleObject = new TileMapRectangleObject(width, height, sharedData.Name, sharedData.Type, sharedData.Id, sharedData.X, sharedData.Y);

                    tileMapObjectLayer.AddObject(rectangleObject);

                }

                layers.Add(tileMapObjectLayer);

            }

            return layers;

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

        private SharedObjectData ReadSharedObjectData(ContentReader input)
        {
            int objId = input.ReadInt32();
            string objName = input.ReadString();
            string objType = input.ReadString();

            float x = input.ReadSingle();
            float y = input.ReadSingle();

            return new SharedObjectData
            {
                Id = objId,
                Name = objName,
                X = x,
                Y = y,
                Type = objType
            };
        }

        private void ReadProperyMap(ContentReader input, PropertyMap propMap)
        {

            int propertyCount = input.ReadInt32();

            for (int p = 0; p < propertyCount; p++)
            {
                string propName = input.ReadString();
                string propVal = input.ReadString();

                propMap.Add(propName, propVal);
            }
        }

    }
}
