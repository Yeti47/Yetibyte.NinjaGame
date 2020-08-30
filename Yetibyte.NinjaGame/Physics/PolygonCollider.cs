using Box2DX.Collision;
using Box2DX.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Yetibyte.NinjaGame.Graphics;

namespace Yetibyte.NinjaGame.Physics
{
    public class PolygonCollider : Collider
    {
        private const int DEBUG_LINE_WIDTH = 3;

        private Vector2[] _vertices;

        public IEnumerable<Vector2> Vertices => _vertices;

        public PolygonCollider(ITransformable transformable, IEnumerable<Vector2> vertices, Body body, Shape shape, IPhysicsManager physics) : base(transformable, body, shape, physics)
        {
            _vertices = vertices.ToArray();
        }

        public override void DebugDraw(SpriteBatch spriteBatch, Texture2D texture, Microsoft.Xna.Framework.Color color)
        {

            for(int i = 0; i < _vertices.Length; i++)
            {
                Vector2 start = _vertices[i];
                Vector2 end = i == _vertices.Length-1 ? _vertices[0] : _vertices[i + 1];

                start += Position;
                end += Position;

                spriteBatch.DrawLine(texture, start, end, color, DEBUG_LINE_WIDTH);
            }

        }
    }
}
