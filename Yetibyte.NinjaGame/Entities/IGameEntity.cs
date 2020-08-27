using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.Core;
using Yetibyte.NinjaGame.Graphics;

namespace Yetibyte.NinjaGame.Entities
{
    public interface IGameEntity : Core.IUpdateable
    {
        RenderLayer RenderLayer { get; }
        int DrawOrder { get; }

        int UpdateOrder { get; }

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);

    }
}
