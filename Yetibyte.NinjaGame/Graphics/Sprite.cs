using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Yetibyte.NinjaGame.Core;

namespace Yetibyte.NinjaGame.Graphics
{
    public class Sprite 
    {
        public int TexturePosX { get; set; }
        public int TexturePosY { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Texture2D Texture { get; private set; }

        public Vector2 Origin { get; set; }

        public Rectangle TextureBounds => new Rectangle(TexturePosX, TexturePosY, Width, Height);

        public Sprite(int texturePosX, int texturePosY, int width, int height, Texture2D texture)
        {
            TexturePosX = texturePosX;
            TexturePosY = texturePosY;
            Width = width;
            Height = height;
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            Origin = new Vector2(width / 2f, height / 2f);
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

            //public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
            //spriteBatch.Draw(Texture, position, TextureBounds, Color.White);

            spriteBatch.Draw(Texture, position, TextureBounds, Color.White, 0, Origin, Vector2.One, SpriteEffects.None, 0);
        }

    }
}
