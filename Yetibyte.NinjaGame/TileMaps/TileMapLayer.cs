namespace Yetibyte.NinjaGame.TileMaps
{
    public abstract class TileMapLayer
    {
        public TileMap ParentTileMap { get; }
        public int Id { get; private set; }
        public string Name { get; set; }

        public abstract TileMapLayerType LayerType { get; }

        protected TileMapLayer()
        {

        }

        protected TileMapLayer(TileMap parentTileMap, int id, string name)
        {
            ParentTileMap = parentTileMap;
            Id = id;
            Name = name;
        }

    }
}
