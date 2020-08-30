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

    public class RectCollider : Collider
    {
        public Vector2 Size { get; }

        public Rectangle BoundingBox => new Rectangle(MathUtil.RoundToInt(Position.X -  Size.X / 2f), MathUtil.RoundToInt(Position.Y - Size.Y/2f), MathUtil.RoundToInt(Size.X), MathUtil.RoundToInt(Size.Y));


        public RectCollider(ITransformable transformable, Body body, Shape shape, Vector2 size, IPhysicsManager physics)
            : base(transformable, body, shape, physics)
        {

            Size = size;

        }

        public override void DebugDraw(SpriteBatch spriteBatch, Texture2D texture, Microsoft.Xna.Framework.Color color)
        {

            spriteBatch.Draw(texture, BoundingBox, color * 0.333f);
        }

    }
}
