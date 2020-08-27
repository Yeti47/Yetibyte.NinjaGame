using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMap
    {
        private List<TileMapLayer> _layers = new List<TileMapLayer>();
        private List<TileSetReference> _tileSetReferences = new List<TileSetReference>();

        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Vector2 Position { get; set; }

        public IEnumerable<TileMapLayer> Layers => new ReadOnlyCollection<TileMapLayer>(_layers);

        public int LayerCount => _layers.Count;
        public int TileSetReferenceCount => _tileSetReferences.Count;

        public IEnumerable<TileSetReference> TileSetReferences => new ReadOnlyCollection<TileSetReference>(_tileSetReferences);

        private TileMap()
        {

        }

        public TileMap(int tileHeight, int tileWidth, int width, int height)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Width = width;
            Height = height;
        }


        public void AddTileSet(int firstGid, TileSet tileSet)
        {
            _tileSetReferences.Add(new TileSetReference(firstGid, tileSet));
        }

        public void AddTileSetReference(int firstGid, string tileSetPath)
        {
            _tileSetReferences.Add(new TileSetReference(firstGid, tileSetPath));
        }

        public void AddTileSetReference(TileSetReference tileSetReference)
        {
            _tileSetReferences.Add(tileSetReference);
        }

        public void LoadTileSets(ContentManager contentManager)
        {
            foreach (var tileSetRef in _tileSetReferences)
                tileSetRef.LoadTileSet(contentManager);
        }

        public void AddLayer(TileMapLayer layer)
        {
            if (layer is null)
            {
                throw new ArgumentNullException(nameof(layer));
            }

            _layers.Add(layer);
        }

        public void AddLayer(TileMapLayerType layerType, int id, string name)
        {
            if (layerType == TileMapLayerType.Tile)
            {
                TileMapTileLayer tileLayer = new TileMapTileLayer(this, id, name, Width, Height);
                _layers.Add(tileLayer);
            }
            else if (layerType == TileMapLayerType.Object)
            {

            }
            else
            {
                throw new ArgumentException("Unknown tile layer type.");
            }
        }

        public TileSetReference GetTileSetByGlobalTileId(int gid)
        {
            return _tileSetReferences.LastOrDefault(tr => tr.FirstGid <= gid);
        }


    }
}
