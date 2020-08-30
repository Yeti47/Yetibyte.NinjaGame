using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.Core;

namespace Yetibyte.NinjaGame.Entities
{
    public interface ITileMapPlaceable : IGameEntity
    {

        Vector2 Position { get; }

        void Initialize(GameServiceContainer gameServices, ContentManager content, PropertyMap propMap, Vector2 position);

    }
}
