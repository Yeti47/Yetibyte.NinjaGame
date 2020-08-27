namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMapPointObject : TileMapObject
    {
        public override TileMapObjectType ObjectType => TileMapObjectType.Point;

        public TileMapPointObject(string name, int id, int x, int y) : base(name, id, x, y)
        {
        }

    }
}
