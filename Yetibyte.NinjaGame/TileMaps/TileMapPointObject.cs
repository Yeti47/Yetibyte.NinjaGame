namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMapPointObject : TileMapObject
    {
        public override TileMapObjectType ObjectType => TileMapObjectType.Point;



        public TileMapPointObject(string name, string type, int id, float x, float y) : base(name, type, id, x, y)
        {
        }

    }
}
