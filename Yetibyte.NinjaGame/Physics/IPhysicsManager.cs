using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Yetibyte.NinjaGame.Physics
{
    public interface IPhysicsManager
    {
        Vector2 Gravity { get; set; }

        PolygonCollider CreatePolygonCollider(ITransformable transformable, IEnumerable<Vector2> vertices, float density);

        RectCollider CreateRectCollider(ITransformable transformable, Vector2 size, bool isDynamic);
        RectCollider CreateRectCollider(ITransformable transformable, Vector2 size, float density);
        void DestroyCollider(RectCollider collider);
        void Update(GameTime gameTime);

        float ToPhysicsUnits(float pixels);

        float ToPixels(float physicsUnits);

        Vector2 ToPhysicsUnits(Vector2 pixelVector);
        Vector2 ToPixels(Vector2 physicsVector);

        Rectangle ToPixels(Rectangle physicsRect);
        Rectangle ToPhysicsUnits(Rectangle pixelRect);

    }
}