using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.Physics;

namespace Yetibyte.NinjaGame.Entities
{
    public class TestObject : ITransformable
    {
        public Vector2 Position { get; set; }

        public IGameEntity GetGameEntity() => null;
    }
}
