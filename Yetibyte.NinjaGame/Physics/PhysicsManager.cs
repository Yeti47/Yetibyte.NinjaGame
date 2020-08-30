using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using Microsoft.Xna.Framework;
using Yetibyte.NinjaGame.Core;

namespace Yetibyte.NinjaGame.Physics
{
    public class PhysicsManager : IPhysicsManager
    {
        public const int DEFAULT_WORLD_BOUNDS_LOWER_X = -1000;
        public const int DEFAULT_WORLD_BOUNDS_LOWER_Y = -1000;

        public const int DEFAULT_WORLD_BOUNDS_UPPER_X = 1000;
        public const int DEFAULT_WORLD_BOUNDS_UPPER_Y = 1000;

        private const int VELOCITY_ITERATIONS = 8;
        private const int POSITION_ITERATIONS = 3;

        private readonly World _world;

        public int PixelsPerUnit { get; }

        public Vector2 Gravity
        {
            get => ToPixels(new Vector2(_world.Gravity.X, _world.Gravity.Y));
            set => _world.Gravity = new Box2DX.Common.Vec2(ToPhysicsUnits(value.X), ToPhysicsUnits(value.Y));
        }

        public PhysicsManager(Vector2 gravity, int ppu, Vector2? lowerWorldBounds = null, Vector2? upperWorldBounds = null)
        {
            PixelsPerUnit = ppu;

            lowerWorldBounds = ToPhysicsUnits( lowerWorldBounds ?? new Vector2(DEFAULT_WORLD_BOUNDS_LOWER_X, DEFAULT_WORLD_BOUNDS_LOWER_Y) );
            upperWorldBounds = ToPhysicsUnits( upperWorldBounds ?? new Vector2(DEFAULT_WORLD_BOUNDS_UPPER_X, DEFAULT_WORLD_BOUNDS_UPPER_Y) );

            AABB boundingBox = new AABB
            {
                LowerBound = new Box2DX.Common.Vec2(lowerWorldBounds.Value.X, lowerWorldBounds.Value.Y),
                UpperBound = new Box2DX.Common.Vec2(upperWorldBounds.Value.X, upperWorldBounds.Value.Y)
            };

            _world = new World(boundingBox, new Box2DX.Common.Vec2(ToPhysicsUnits(gravity.X), ToPhysicsUnits(gravity.Y)), false);
        }

        public float ToPhysicsUnits(float pixels) => pixels / PixelsPerUnit;

        public float ToPixels(float physicsUnits) => physicsUnits * PixelsPerUnit;

        public Vector2 ToPhysicsUnits(Vector2 pixelVector) => new Vector2(ToPhysicsUnits(pixelVector.X), ToPhysicsUnits(pixelVector.Y));
        public Vector2 ToPixels(Vector2 physicsVector) => new Vector2(ToPixels(physicsVector.X), ToPixels(physicsVector.Y));

        public Rectangle ToPixels(Rectangle physicsRect) => new Rectangle()
        {
            X = MathUtil.RoundToInt(ToPixels(physicsRect.X)),
            Y = MathUtil.RoundToInt(ToPixels(physicsRect.Y)),
            Width = MathUtil.RoundToInt(ToPixels(physicsRect.Width)),
            Height = MathUtil.RoundToInt(ToPixels(physicsRect.Height)),
        };

        public Rectangle ToPhysicsUnits(Rectangle pixelRect) => new Rectangle()
        {
            X = MathUtil.RoundToInt(ToPhysicsUnits(pixelRect.X)),
            Y = MathUtil.RoundToInt(ToPhysicsUnits(pixelRect.Y)),
            Width = MathUtil.RoundToInt(ToPhysicsUnits(pixelRect.Width)),
            Height = MathUtil.RoundToInt(ToPhysicsUnits(pixelRect.Height)),
        };

        public void DestroyCollider(RectCollider collider)
        {
            if (collider is null)
            {
                throw new ArgumentNullException(nameof(collider));
            }

            _world.DestroyBody(collider.Body);

        }

        public PolygonCollider CreatePolygonCollider(ITransformable transformable, IEnumerable<Vector2> vertices, float density)
        {
            BodyDef bodyDef = new BodyDef
            {
                UserData = transformable,
                Angle = 0,
                IsBullet = false,
                Position = new Box2DX.Common.Vec2(ToPhysicsUnits(transformable.Position.X), ToPhysicsUnits(transformable.Position.Y)),
                FixedRotation = true,
                IsSleeping = false,
            };

            Body body = _world.CreateBody(bodyDef);

            PolygonDef polygonDef = new PolygonDef
            {
                Density = density,
                Friction = 0,
                Vertices = vertices
                    .Select(v => new Vec2(ToPhysicsUnits(v.X), ToPhysicsUnits(v.Y)))
                    .ToArray(),
                VertexCount = vertices.Count()
            };

            var fix = body.CreateFixture(polygonDef);

            Shape shape = fix.Shape;
            //PolygonShape polygonShape = shape as PolygonShape;
            //polygonShape.Set(polygonDef.Vertices, polygonDef.Vertices.Length);

            body.SetMassFromShapes();

            PolygonCollider polygonCollider = new PolygonCollider(transformable, vertices, body, shape, this);

            return polygonCollider;

        }

        public RectCollider CreateRectCollider(ITransformable transformable, Vector2 size, bool isDynamic)
        {
            return CreateRectCollider(transformable, size, isDynamic ? 1 : 0);
        }

        public RectCollider CreateRectCollider(ITransformable transformable, Vector2 size, float density)
        {
            BodyDef bodyDef = new BodyDef
            {
                UserData = transformable,
                Angle = 0,
                IsBullet = false,
                Position = new Box2DX.Common.Vec2(ToPhysicsUnits(transformable.Position.X), ToPhysicsUnits(transformable.Position.Y)),
                FixedRotation = true,
                IsSleeping = false,
                //MassData = new MassData { Mass = mass }
            };

            Body body = _world.CreateBody(bodyDef);

            PolygonDef polygonDef = new PolygonDef
            {
                Density = density,
                Friction = 0
            };

            polygonDef.SetAsBox(ToPhysicsUnits(size.X / 2f), ToPhysicsUnits(size.Y / 2f));

            var fix = body.CreateFixture(polygonDef);

            Shape shape = fix.Shape;

            body.SetMassFromShapes();

            RectCollider collider = new RectCollider(transformable, body, shape, size, this);

            return collider;
        }

        public void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds, VELOCITY_ITERATIONS, POSITION_ITERATIONS);

        }


    }
}
