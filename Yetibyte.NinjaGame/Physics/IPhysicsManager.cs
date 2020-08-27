using Microsoft.Xna.Framework;

namespace Yetibyte.NinjaGame.Physics
{
    public interface IPhysicsManager
    {
        Vector2 Gravity { get; set; }

        Collider CreateCollider(ITransformable transformable, float mass, Vector2 size, bool isDynamic);
        Collider CreateCollider(ITransformable transformable, float mass, Vector2 size, float density);
        void DestroyCollider(Collider collider);
        void Update(GameTime gameTime);

        float ToPhysicsUnits(float pixels);

        float ToPixels(float physicsUnits);

        Vector2 ToPhysicsUnits(Vector2 pixelVector);
        Vector2 ToPixels(Vector2 physicsVector);

        Rectangle ToPixels(Rectangle physicsRect);
        Rectangle ToPhysicsUnits(Rectangle pixelRect);

    }
}