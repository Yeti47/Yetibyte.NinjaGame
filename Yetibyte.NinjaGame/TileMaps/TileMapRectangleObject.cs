using Microsoft.Xna.Framework;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMapRectangleObject : TileMapObject
    {

        public float Width { get; private set; }
        public float Height { get; private set; }

        public Vector2 Size => new Vector2(Width, Height);

        public override TileMapObjectType ObjectType => TileMapObjectType.Rectangle;


        public TileMapRectangleObject(float width, float height, string name, string type, int id, float x, float y) : base(name, type, id, x, y)
        {
            Width = width;
            Height = height;
        }

    }
}
