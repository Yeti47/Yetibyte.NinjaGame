using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.Core;
using Yetibyte.NinjaGame.Graphics;
using Yetibyte.NinjaGame.Physics;

namespace Yetibyte.NinjaGame.Entities
{
    public class Rock : ITileMapPlaceable, ITransformable
    {
        private const string SPRITE_SHEET_PATH = "Textures/rock-break-spritesheet";

        private const string DEBUG_TEXTURE_PATH = "Textures/white_pixel";

        private const int SPRITE_SIZE = 64;
        private const int ROCK_SIZE = 32;

        private Texture2D _spriteSheet;
        private Texture2D _debugTexture;

        public Vector2 Position { get; set; }

        public RenderLayer RenderLayer { get; set; }

        public int DrawOrder { get; set; }

        public int UpdateOrder { get; set; }

        public string Tag { get; private set; }
        public int Health { get; private set; }

        public Sprite Sprite { get; private set; }

        public RectCollider Collider { get; private set; }

        public Rock()
        {
        }

        public void Initialize(GameServiceContainer gameServices, ContentManager content, PropertyMap propMap, Vector2 position)
        {
            Health = propMap.GetInt("Health");
            Tag = propMap["Tag"];

            _spriteSheet = content.Load<Texture2D>(SPRITE_SHEET_PATH);

            Sprite = new Sprite(0, 0, SPRITE_SIZE, SPRITE_SIZE, _spriteSheet);

            Position = position;

            IPhysicsManager physicsManager = gameServices.GetService<IPhysicsManager>();

            this.Collider = physicsManager.CreateRectCollider(this, new Vector2(ROCK_SIZE), 500);

            _debugTexture = content.Load<Texture2D>(DEBUG_TEXTURE_PATH);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite?.Draw(spriteBatch, Position);
            //Collider?.DebugDraw(spriteBatch, _debugTexture, Color.DarkRed);
        }

        public void Update(GameTime gameTime)
        {
            Position = Collider.Position;
        }

        public IGameEntity GetGameEntity() => this;

    }
}
