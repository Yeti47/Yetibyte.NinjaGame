using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMapObjectLayer : TileMapLayer
    {

        private readonly List<TileMapObject> _objects = new List<TileMapObject>();

        public IEnumerable<TileMapObject> Objects => new ReadOnlyCollection<TileMapObject>(_objects);

        public override TileMapLayerType LayerType => TileMapLayerType.Object;

        public TileMapObjectLayer(TileMap parentTileMap, int id, string name)
            :base(parentTileMap, id, name)
        {

        }

        public void AddObject(TileMapObject tileMapObject)
        {
            if (tileMapObject is null)
            {
                throw new System.ArgumentNullException(nameof(tileMapObject));
            }

            _objects.Add(tileMapObject);
        }

        public void RemoveObject(TileMapObject tileMapObject)
        {
            if (!HasObject(tileMapObject))
                return;

            _objects.Remove(tileMapObject);
        }

        public bool HasObject(TileMapObject tileMapObject) => _objects.Contains(tileMapObject);


    }

}
