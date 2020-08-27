using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.Core;

namespace Yetibyte.NinjaGame.Entities
{
    public interface ITileMapPlaceable : IGameEntity
    {
        void Initialize(ContentManager content, PropertyMap propMap);

    }
}
