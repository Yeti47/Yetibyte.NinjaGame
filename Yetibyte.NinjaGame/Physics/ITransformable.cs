using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.Entities;

namespace Yetibyte.NinjaGame.Physics
{
    public interface ITransformable
    {
        Vector2 Position { get; set; }

        IGameEntity GetGameEntity();

    }
}
