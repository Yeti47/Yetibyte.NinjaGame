using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yetibyte.NinjaGame.Core;
using Yetibyte.NinjaGame.Graphics;
using Yetibyte.NinjaGame.Physics.RayCasting;

namespace Yetibyte.NinjaGame.Physics
{

    public class DefaultContactListener : ContactListener
    {
        public void BeginContact(Contact contact)
        {
        }

        public void EndContact(Contact contact)
        {
        }

        public void PostSolve(Contact contact, ContactImpulse impulse)
        {
        }

        public void PreSolve(Contact contact, Manifold oldManifold)
        {
        }
    }

    public class SpriteBatchDebugDraw : DebugDraw
    {
        private IPhysicsManager _physicsManager;
        private SpriteBatch _spriteBatch;
        private readonly Texture2D _debugTexture;

        public SpriteBatchDebugDraw(IPhysicsManager physicsManager, SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            _physicsManager = physicsManager;
            _spriteBatch = spriteBatch;
            this._debugTexture = debugTexture;
        }

        public override void DrawCircle(Vec2 center, float radius, Box2DX.Dynamics.Color color)
        {
        }

        public override void DrawPolygon(Vec2[] vertices, int vertexCount, Box2DX.Dynamics.Color color)
        {

            for (int i = 0; i < vertexCount; i++)
            {
                Vector2 start = new Vector2(vertices[i].X, vertices[i].Y);
                start = _physicsManager.ToPixels(start);

                Vector2 end = i == vertexCount - 1 ? (new Vector2(vertices[0].X, vertices[0].Y)) : (new Vector2(vertices[i+1].X, vertices[i+1].Y));
                end = _physicsManager.ToPixels(end);

                Microsoft.Xna.Framework.Color col = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, 1);

                _spriteBatch.DrawLine(_debugTexture, start, end, col, 3);
            }

        }

        public override void DrawSegment(Vec2 p1, Vec2 p2, Box2DX.Dynamics.Color color)
        {
            Vector2 start = new Vector2(p1.X, p1.Y);
            start = _physicsManager.ToPixels(start);

            Vector2 end = new Vector2(p2.X, p2.Y);
            end = _physicsManager.ToPixels(end);

            Microsoft.Xna.Framework.Color col = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, 1);
           
            _spriteBatch.Begin(SpriteSortMode.Immediate);
            _spriteBatch.DrawLine(_debugTexture, start, end, col, 3);
            _spriteBatch.End();
        }

        public override void DrawSolidCircle(Vec2 center, float radius, Vec2 axis, Box2DX.Dynamics.Color color)
        {

        }

        public override void DrawSolidPolygon(Vec2[] vertices, int vertexCount, Box2DX.Dynamics.Color color)
        {

        }

        public override void DrawXForm(XForm xf)
        {

        }
    }

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
            _world.SetContactListener(new DefaultContactListener());

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
                //UserData = transformable,
                Angle = 0,
                IsBullet = false,
                Position = new Box2DX.Common.Vec2(ToPhysicsUnits(transformable.Position.X), ToPhysicsUnits(transformable.Position.Y)),
                FixedRotation = true,
                IsSleeping = false, AllowSleep = false
            };

            Body body = _world.CreateBody(bodyDef);

            PolygonDef polygonDef = new PolygonDef
            {
                Density = density,
                Friction = 1,
                Vertices = vertices
                    .Select(v => new Vec2(ToPhysicsUnits(v.X), ToPhysicsUnits(v.Y)))
                    .ToArray(),
                VertexCount = vertices.Count(), 
                Restitution = 0
            };

            var fix = body.CreateFixture(polygonDef);
            Shape shape = fix.Shape;
            
            //PolygonShape polygonShape = shape as PolygonShape;
            //polygonShape.Set(polygonDef.Vertices, polygonDef.Vertices.Length);

            body.SetMassFromShapes();
            
            PolygonCollider polygonCollider = new PolygonCollider(transformable, vertices, body, shape, this);

            body.SetUserData(polygonCollider);

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
                //UserData = transformable,
                Angle = 0,
                IsBullet = false,
                Position = new Box2DX.Common.Vec2(ToPhysicsUnits(transformable.Position.X), ToPhysicsUnits(transformable.Position.Y)),
                FixedRotation = true,
                IsSleeping = false
                //MassData = new MassData { Mass = mass }
            };
            
            Body body = _world.CreateBody(bodyDef);

            PolygonDef polygonDef = new PolygonDef
            {
                Density = density,
                Friction = 1, 
                Restitution = 0, Type = ShapeType.PolygonShape
            };

            polygonDef.SetAsBox(ToPhysicsUnits(size.X / 2f), ToPhysicsUnits(size.Y / 2f));

            var fix = body.CreateFixture(polygonDef);
            
            Shape shape = fix.Shape;

            body.SetMassFromShapes();

            RectCollider collider = new RectCollider(transformable, body, shape, size, this);

            body.SetUserData(collider);

            return collider;
        }

        public void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds, VELOCITY_ITERATIONS, POSITION_ITERATIONS);
            _world.Validate();

        }

        public RayCastResult RayCastSingle(Vector2 start, Vector2 end)
        {
            Segment segment = new Segment
            {
                P1 = new Vec2(ToPhysicsUnits(start.X), ToPhysicsUnits(start.Y)),
                P2 = new Vec2(ToPhysicsUnits(end.X), ToPhysicsUnits(end.Y))
            };
            
            var fixture = _world.RaycastOne(segment, out float fraction, out Vec2 normal, false, null);
            
            Vector2 normalMg = new Vector2(normal.X, normal.Y);
            
            Collider collider = (fixture?.Body?.GetUserData() as Collider);

            return new RayCastResult(collider, normalMg, fraction, start, end);
        }

        public RayCastResult RayCast(Vector2 start, Vector2 end, int maxHitCount)
        {
            Segment segment = new Segment
            {
                P1 = new Vec2(ToPhysicsUnits(start.X), ToPhysicsUnits(start.Y)),
                P2 = new Vec2(ToPhysicsUnits(end.X), ToPhysicsUnits(end.Y))
            };

            _world.Raycast(segment, out Fixture[] fixtures, maxHitCount, false, null);
            //_world.Raycast(segment, out Fixture[] fixtures, maxHitCount, false, null);

            IEnumerable<Collider> colliders = fixtures?.Where(f => f != null).Select(f => f?.Body?.GetUserData() as Collider);

            return new RayCastResult(colliders, Vector2.Zero, 0, start, end);

        }

        public void EnableDebugDraw(SpriteBatch spriteBatch, Texture2D debugTexture)
        {

            SpriteBatchDebugDraw spriteBatchDebugDraw = new SpriteBatchDebugDraw(this, spriteBatch, debugTexture);
            spriteBatchDebugDraw.Flags = DebugDraw.DrawFlags.Shape | DebugDraw.DrawFlags.Aabb | DebugDraw.DrawFlags.Shape;
            _world.SetDebugDraw(spriteBatchDebugDraw);
            
        }


    }
}
