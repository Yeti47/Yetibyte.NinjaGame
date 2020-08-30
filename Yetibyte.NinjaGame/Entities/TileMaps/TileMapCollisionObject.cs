using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yetibyte.NinjaGame.Graphics;
using Yetibyte.NinjaGame.Physics;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.Entities.TileMaps
{
    public class TileMapCollisionObject : IGameEntity, ITransformable
    {
        private Texture2D _debugTexture;

        public RenderLayer RenderLayer => RenderLayer.One;

        public int DrawOrder => 0;

        public int UpdateOrder => 0;

        public Collider Collider { get; set; }
        public Vector2 Position { get; set; }

        public bool IsDebugDrawEnabled { get; set; }

        public TileMapCollisionObject(TileMapObject tileMapObj, Texture2D debugTexture)
        {
            Position = new Vector2(tileMapObj.X, tileMapObj.Y);

            if(tileMapObj.ObjectType == TileMapObjectType.Rectangle)
            {
                TileMapRectangleObject rectObj = (TileMapRectangleObject)tileMapObj;
                Position += rectObj.Size / 2f;
            }

            _debugTexture = debugTexture;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if(IsDebugDrawEnabled)
                Collider?.DebugDraw(spriteBatch, _debugTexture, Color.Black * 0.5f);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
