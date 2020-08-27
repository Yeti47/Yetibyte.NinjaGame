using Box2DX.Collision;
using Box2DX.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using Yetibyte.NinjaGame.Core;

namespace Yetibyte.NinjaGame.Physics
{
    public class Collider
    {
        private readonly Body _body;
        private readonly Shape _shape;
        private readonly IPhysicsManager _physics;

        public Vector2 Size { get; }

        public float Mass => _body.GetMass();

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

        public Rectangle BoundingBox => new Rectangle(MathUtil.RoundToInt(Position.X -  Size.X / 2f), MathUtil.RoundToInt(Position.Y - Size.Y/2f), MathUtil.RoundToInt(Size.X), MathUtil.RoundToInt(Size.Y));

        public Body Body => _body;

        public Collider(ITransformable transformable, Body body, Shape shape, Vector2 size, IPhysicsManager physics)
        {
            _body = body;
            _shape = shape;
            Size = size;
            this._physics = physics;
            Transformable = transformable;
        }

        public void DebugDraw(SpriteBatch spriteBatch, Texture2D texture, Microsoft.Xna.Framework.Color color)
        {

            spriteBatch.Draw(texture, BoundingBox, color);
        }

        public void Update(GameTime gameTime)
        {
            //if(Transformable != null)
            //    Transformable.Position = Position;
        }

        public void ApplyForce(Vector2 force)
        {
            force = _physics.ToPhysicsUnits(force);
            _body.ApplyForce(new Box2DX.Common.Vec2(force.X, force.Y), _body.GetWorldCenter());
        }

        public void ApplyImpulse(Vector2 impulse)
        {
            impulse = _physics.ToPhysicsUnits(impulse);
            _body.ApplyImpulse(new Box2DX.Common.Vec2(impulse.X, impulse.Y), 
                _body.GetWorldCenter());
        }

        

    }
}
