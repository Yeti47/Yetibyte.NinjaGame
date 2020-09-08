using Box2DX.Collision;
using Box2DX.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Yetibyte.NinjaGame.Entities;

namespace Yetibyte.NinjaGame.Physics
{

    public abstract class Collider
    {
        protected readonly Body _body;
        protected readonly Shape _shape;
        protected readonly IPhysicsManager _physics;

        public virtual float Mass => _body.GetMass();

        public Vector2 Velocity
        {
            get
            {
                var velo = _body.GetLinearVelocity();
                return _physics.ToPixels(new Vector2(velo.X, velo.Y));
            }

            set => _body.SetLinearVelocity(new Box2DX.Common.Vec2(_physics.ToPhysicsUnits(value.X), _physics.ToPhysicsUnits(value.Y)));
        }

        public Vector2 Position
        {
            get
            {
                return _physics.ToPixels(new Vector2(_body.GetPosition().X, _body.GetPosition().Y));
            }

        }

        public ITransformable Transformable { get; private set; }



        public Body Body => _body;

        protected Collider(ITransformable transformable, Body body, Shape shape, IPhysicsManager physics)
        {
            _body = body;
            _shape = shape;
            this._physics = physics;
            Transformable = transformable;
        }

        public abstract void DebugDraw(SpriteBatch spriteBatch, Texture2D texture, Microsoft.Xna.Framework.Color color);

        public void Update(GameTime gameTime)
        {
            //if(Transformable != null)
            //    Transformable.Position = Position;
        }

        public virtual void ApplyForce(Vector2 force)
        {
            force = _physics.ToPhysicsUnits(force);
            _body.ApplyForce(new Box2DX.Common.Vec2(force.X, force.Y), _body.GetWorldCenter());
        }

        public virtual void ApplyImpulse(Vector2 impulse)
        {
            impulse = _physics.ToPhysicsUnits(impulse);
            _body.ApplyImpulse(new Box2DX.Common.Vec2(impulse.X, impulse.Y),
                _body.GetWorldCenter());
        }

        public void MoveTo(Vector2 position)
        {
            Vector2 physicsPosition = _physics.ToPhysicsUnits(position);
            _body.SetPosition(new Box2DX.Common.Vec2(physicsPosition.X, physicsPosition.Y));

        }

    }
}
