using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Yetibyte.NinjaGame.TileMaps
{

    public class TileMapPolygonObject : TileMapObject
    {
        private Vector2[] _vertices;

        public IEnumerable<Vector2> Vertices => _vertices;

        public int VertexCount => _vertices.Length;

        public override TileMapObjectType ObjectType => TileMapObjectType.Polygon;


        public TileMapPolygonObject(IEnumerable<Vector2> vertices, string name, string type, int id, float x, float y) : base(name, type, id, x, y)
        {
            _vertices = vertices.ToArray();
        }

    }
}
